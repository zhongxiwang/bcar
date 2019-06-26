

var BASEDATA = [
    { title: "奥亚酒店", content: "北苑路169号", point: "116.0115408899|29.673370055466", isOpen: 0, icon: "http://static.blog.csdn.net/images/medal/holdon_s.gif" },
    { title: "珀丽酒店", content: "将台西路8号", point: "116.484289|29.97936", isOpen: 0, icon: "http://static.blog.csdn.net/images/medal/holdon_s.gif" }
]

	var CurrentPositionErrorList={
		"BMAP_STATUS_SUCCESS":"检索成功。",
		"BMAP_STATUS_CITY_LIST":"城市列表。",
		"BMAP_STATUS_UNKNOWN_LOCATION":"位置结果未知。",
		"BMAP_STATUS_UNKNOWN_ROUTE":"导航结果未知",
		"BMAP_STATUS_INVALID_KEY":"非法密钥",
		"BMAP_STATUS_INVALID_REQUEST":"非法请求",
		"BMAP_STATUS_PERMISSION_DENIED":"没有权限",
		"BMAP_STATUS_SERVICE_UNAVAILABLE":"服务不可用",
		"BMAP_STATUS_TIMEOUT":"超时"
	};
var myIcon = new BMap.Icon("../image/car.png", new BMap.Size(50, 30));

var userinfo = {
    currentInfo: {
        location: { lng: "", lat: "" },
        name: "",
    },
    carType: -1,//1快车，2顺风车，3专车
    City: "",
    address:"",
    targetLocation: {
        location: { lng: "", lat: "" },
        name:""
    },
    userInfo: {
        mobile: "",
        userName: "",
        personNum:0,//乘车人数
    },
    distance: "",
    timelength:""
};


var map = null;
var goc = null;//地址解析
function Init()
{
    $.ajax({
        url: "/api/DriverLocation",
        success: function (data) {
            
            addCar(data);
        }
    });
    map= new BMap.Map("allmap");
    var point = new BMap.Point(116.331398, 39.897445);
    map.centerAndZoom(point, 18);
    map.enableScrollWheelZoom();
    geoc = new BMap.Geocoder();
    //addCar(BASEDATA);
    var geolocation = new BMap.Geolocation();
    geolocation.getCurrentPosition(function (r) {
        if (this.getStatus() == BMAP_STATUS_SUCCESS) {
            var mk = new BMap.Marker(r.point);
            map.addOverlay(mk);
            map.panTo(r.point);
            userinfo.currentInfo.location.lng = r.point.lng;
            userinfo.currentInfo.location.lat = r.point.lat;
            userinfo.City = r.address.city;
            userinfo.address = r.address;
            setInput("suggestId", "searchResultPanel");
            setInput("suggestId2", "searchResultPanel2");
            G("suggestId").value = "当前位置";
            userinfo.currentlocation = getL(r.address.province) + getL(r.address.city) + getL(r.address.district) + getL(r.address.street) + getL( r.address.streetNumber);
        }else {
            alert(CurrentPositionErrorList[this.getStatus]);
        }
    }, { enableHighAccuracy: true });
}




function getL(n) {
    if (n === undefined || n == null) return "";
    else return n;
}

///通过坐标取得地名
function getLocationName(pt) {
    //var point = new BMap.Point(116.331398, 39.897445);
    geoc.getLocation(pt, function (rs) {
        console.log(rs);
        var addComp = rs.addressComponents;
        //alert(addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber);
    });      
}
function getLoctionPoint(City, Name,fun) {
    var myGeo = new BMap.Geocoder();
     myGeo.getPoint(Name, function (point) {
        if (point) {
            //map.centerAndZoom(point, 16);
            //var marker2 = new BMap.Marker(point, { icon: myIcon });
            //map.addOverlay(new BMap.Marker(point));
            fun(point);
        } else {
            alert("您选择地址没有解析到结果!");
        }
    }, City);
}




var output;

function routegh(v1,v2)
{
    var searchComplete = function (results) {

        if (transit.getStatus() != BMAP_STATUS_SUCCESS) {
            return;
        }
        var plan = results.getPlan(0);
        userinfo.timelength = plan.getDuration(true);
        userinfo.distance = plan.getDistance(true);
    }
    var transit = new BMap.DrivingRoute(map, {
        renderOptions: { map: map },
        onSearchComplete: searchComplete,
        onPolylinesSet: function () {
            setTimeout(function () {
                var price = 0;
                $.ajax({
                    url: "/api/LocationSite/getPrice?distance=" + userinfo.distance + "&type=4",
                    async: false,
                    success: function (data)
                    {
                        price = data;
                    }
                });
                window.parent.$.confirm("全长" + userinfo.distance + ",需要费用" + price + "元,是否创建订单？", function () {
                    window.parent.dataset.rider = document.getElementById("call").value;
                    window.parent.dataset.startingPoint = userinfo.currentInfo.name;
                    window.parent.dataset.endingPoint = userinfo.targetLocation.name;
                    window.parent.dataset.price = price;
                    userinfo.targetLocation.price = price;
                    window.parent.submit();
                    search();
                    //sub = true;
                });

            }, "1000");
        }
    });
    transit.search(v1,v2);
}


///输入框数据
function setInput(id,resuiltid)
{
    var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
        {
            "input": id
            ,"location": map
        });

    ac.addEventListener("onhighlight", function (e) {  
        var str = "";
        var _value = e.fromitem.value;
        var value = "";
        if (e.fromitem.index > -1) {
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;

        value = "";
        if (e.toitem.index > -1) {
            _value = e.toitem.value;
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;
        G(resuiltid).innerHTML = str;
    });
}

function setPlace() {
    map.clearOverlays();    //清除地图上所有覆盖物
    function myFun() {
        var pp = local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
        map.centerAndZoom(pp, 18);
        map.addOverlay(new BMap.Marker(pp));    //添加标注
    }
    var local = new BMap.LocalSearch(map, { //智能搜索
        onSearchComplete: myFun
    });
    local.search(myValue);
}

function G(id) {
	return document.getElementById(id);
}
Init();

function addMark(p1,p2)
{
    var point = new BMap.Point(p1,p2);
    var marker = new BMap.Marker(point, { icon: myIcon });
    //marker.setLabel(label);
    map.addOverlay(marker);
}

function addCar(data) {
    map.clearOverlays();
    for (var i = 0; i < data.length; i++) {
        var json = data[i];
        //var res = json.point.split("|");
        //var point = new BMap.Point(res[0], res[1]);
        var point = new BMap.Point(json.pointx, json.pointy);
        //var iconImg = new BMap.Icon(json.icon, new BMap.Size(22, 22));
        var marker = new BMap.Marker(point, { icon: myIcon });
        //var iw = createInfoWindow(json);
        //var label = new BMap.Label(json.title, { "offset": new BMap.Size(22, 22) });
        //marker.setLabel(label);
        map.addOverlay(marker);
        
    }
}
//创建InfoWindow
function createInfoWindow(json) {
    var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>" + json.content + "</div>");
    return iw;
}