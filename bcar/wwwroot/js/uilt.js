

/*
 *获取js ticket
 * */
function getApiTicket() {
    var result = getCookie("ticket");
    if (result != null) return result;
    $.ajax({
        url: "/api/Token/apiticket",
        type: "GET",
        sucess: function (data)
        {
            result = data;
            setCookie("ticket", data, "s7000");
        }
    });
    return result;
}



/**
 * 获取cookie
 * @param {any} name
 */
function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}

/**
 * 计算cookie
 * @param {s20是代表20秒,h是指小时,d是天数} name
 * @param {any} value
 * @param {any} time
 */
function setCookie(name, value, time) {
    var strsec = getsec(time);
    var exp = new Date();
    exp.setTime(exp.getTime() + strsec * 1);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
///计算时间
function getsec(str) {
    var str1 = str.substring(1, str.length) * 1;
    var str2 = str.substring(0, 1);
    if (str2 == "s") {
        return str1 * 1000;
    }
    else if (str2 == "h") {
        return str1 * 60 * 60 * 1000;
    }
    else if (str2 == "d") {
        return str1 * 24 * 60 * 60 * 1000;
    }
}


var urlTools = {
    //获取RUL参数值
    getUrlParam: function (name) {               /*?videoId=identification  */
        var params = decodeURI(window.location.search);        /* 截取？号后面的部分    index.html?act=doctor,截取后的字符串就是?act=doctor  */
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = params.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },
    Config: function (key) {
        var result = null;
        $({
            url: "/api/config?key=" + key,
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    }
};

var uilt_glob_openid = null;
var userwxinfo = {
    headimgurl: "",
    username: "",
    wxCount: "",
    qrCode: "",
    recommender:""
};


function getWxInfo() {
    
    $.ajax({
        url: "/api/Token/webtoken",
        type: "GET",
        success: function (data) {
            var json = JSON.parse(data);
            userwxinfo.headimgurl = json["headimgurl"];
            userwxinfo.username = json["nickname"];
            userwxinfo.wxCount = json["openid"];
            userwxinfo.recommender = json["qr_scene_str"];
            uilt_glob_openid = userwxinfo.wxCount;
        }
    });
}

/**
 * 用户注册*/
function registeruser()
{
    $.ajax({
        url: "/api/Token/webtoken",
        type:"GET",
        success: function (data) {
            var json = JSON.parse(data);
            userwxinfo.headimgurl = json["headimgurl"];
            userwxinfo.username = json["nickname"];
            userwxinfo.wxCount = json["openid"];
            userwxinfo.recommender = json["qr_scene_str"];
            uilt_glob_openid = userwxinfo.wxCount;
        }
    });

    //$.ajax({
    //    url: "/userisregister?wxcount=" + userwxinfo.wxCount,
    //    async: true,
    //    type: "GET",
    //    success: function (data) {
    //        if (data === 0) {
    //            $.ajax({
    //                url: "/api/Token/ShareQrCode?openid=" + userwxinfo.wxCount,
    //                async: false,
    //                success: function (data)
    //                {
    //                    userwxinfo.qrCode = data;
    //                }
    //            });
    //            $.ajax({
    //                url: "/api/userinfo",
    //                type: "POST",
    //                contentType:"application/json-patch+json",
    //                data: JSON.stringify(userwxinfo),
    //                success: function (data) {

    //                }

    //            });
    //        }
    //    }
    //});

}

/**
 * 
 * 数据是不是空
 * @param {any} str
 */
function isEmptOrNull(str) {
    if (typeof (t) === typeof ("")) str=str.trim();
    if (str === "" || str === undefined || str == null)
        return true;
    return false;
}

var databaseUserinfo = null;

function getuserinfo() {
    $.ajax({
        url: "/userwxgetuserinfo",
        type: "GET",
        success: function (data)
        {
            databaseUserinfo = data;
        }
    });
}

/**
 * 时间格式化
 * */
function DateFormat() {
    let date = new Date(),
        currentDate,
        currentTime,
        seperator = "-", // 如果想要其他格式 只需 修改这里 
        year = date.getFullYear(),
        month = date.getMonth() + 1,
        day = date.getDate(),
        hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours(),
        minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes(),
        second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
    month >= 1 && month <= 9 ? (month = "0" + month) : "";
    day >= 0 && day <= 9 ? (day = "0" + day) : "";
    //当前 日期
    currentDate = year + seperator + month + seperator + day;
    //当前 时间
    currentTime = hour + ":" + minute + ":" + second;
    // 输出格式 为 2018-8-27 14:45:33
    //console.log(currentDate + " " + currentTime);
    return currentDate + " " + currentTime;
}

/**
 * 
 * 发送消息
 * @param {any} Message 消息
 * @param {any} wxcount 微信openid
 */
function SendMessage(Message,wxcount)
{
    $.ajax({
        url: "/api/Token/SendMessage?openid=" + wxcount,
        type: "POST",
        data: {
            message: Message
        },
        success: function (dat) {

        }
    });
}

/**
 * 
 * 是否超时
 * @param {any} dt 时间字符串
 */
function IsOutTime(dt) {
    var dt = new Date();
    var dt2 = new Date(dt);
    var min = dt2.getMinutes() + 5;
    dt2.setMinutes(min);
    if (dt >= dt2) return true;
    else return false;
}