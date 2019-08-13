
var options = {
    'showButton': true,//是否显示定位按钮
    'buttonPosition': 'LB',//定位按钮的位置
    /* LT LB RT RB */
    'buttonOffset': new AMap.Pixel(10, 20),//定位按钮距离对应角落的距离
    'showMarker': true,//是否显示定位点
    'markerOptions': {//自定义定位点样式，同Marker的Options
        'offset': new AMap.Pixel(-18, -36),
        'content': '<img src="https://a.amap.com/jsapi_demos/static/resource/img/user.png" style="width:36px;height:36px"/>'
    },
    'showCircle': true,//是否显示定位精度圈
    'circleOptions': {//定位精度圈的样式
        'strokeColor': '#0093FF',
        'noSelect': true,
        'strokeOpacity': 0.5,
        'strokeWeight': 1,
        'fillColor': '#02B0FF',
        'fillOpacity': 0.25
    }
}
var map;
var geolocation;
function Init() {
    map.plugin('AMap.Geolocation', function () {
        geolocation = new AMap.Geolocation({
            enableHighAccuracy: true,
            timeout: 10000,
            buttonOffset: new AMap.Pixel(10, 20),
            zoomToAccuracy: true,
            buttonPosition: 'RB'
        });
    })
}



/**
 * 
 * 插件注册
 * @param {any} pluginName
 * @param {any} bianL
 */
function plugin(pluginName,bianL) {

}

function InitCar() {
    var point = new BMap.Point(116.331398, 39.897445);
}

function marker(x, y) {
    return new AMap.Marker({
        position: new AMap.LngLat(116.39, 39.92),
        icon: '../image/car.png',
        offset: new AMap.Pixel(-13, -30)
    });
}
/**
 * 清除地图上所有添加的覆盖物
 * */
 function removeAllOverlay() {

    // 清除地图上所有添加的覆盖物
    map.clearMap();
}
/**
 * 获取当前坐标位置
 * @param {成功后回调} callbak
 * @param {错误回调} error
 */
function getCurrent(callbak) {
    var geolocation = new AMap.Geolocation(options);
    map.addControl(geolocation);
    geolocation.getCurrentPosition();
    alert(geolocation);
}

/*
 * 地址名转坐标系
 */
function LocationToPoint(Name) {

}

/**
 * 
 * 在地图上绘制，轨迹
 * @param { 开始地址 } startPoint
 * @param { 结束地址 } EndPoint
 */
function IrouteName(startPoint, EndPoint,city) {
    driving.search(
        [
            { keyword: startPoint, city: city },
            { keyword: EndPoint, city: city }
        ],function (status, result) {
            if (status === 'complete')
            {
                var price = 0;
                $.ajax({
                    url: "/api/LocationSite/getPrice?distance=" + result.routes[0].distance + "&type=4",
                    async: false,
                    success: function (data) {
                        price = data;
                        var distance = "0";
                        if (result.routes[0].distance > 1000) distance = (result.routes[0].distance / 1000) + "千米";
                        else distance = (result.routes[0].distance ) + "米";
                        window.parent.$.confirm("全长" + distance + ",需要费用" + price + "元,是否创建订单？", function () {
                            window.parent.dataset.rider = document.getElementById("mobile").value;
                            window.parent.dataset.startingPoint = startPoint;
                            window.parent.dataset.endingPoint = EndPoint;
                            window.parent.dataset.price = price;
                            //userinfo.targetLocation.price = price;
                            window.parent.submit();
                            search();
                            //sub = true;
                        });
                    }
                });
            } else {
                console.log(result);
            }
    });
}

function search() {
    $.ajax({
        url: "/api/DriverLocation/nearbydriver?lat=" + userinfo.currentInfo.location.lat + "&lng=" + userinfo.currentInfo.location.lng + "&id=" + window.parent.orderId,
        success: function (data) {
            //console.log(data);
        }
    });
}