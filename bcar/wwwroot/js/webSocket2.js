


var MessageType = { uploadLocation: "uploadLocation", orderMessage: "orderMessage" }
/**
 * 消息结构
 */
var Struct = function () {
    return { "Head": { "KeyID": null, "RequestKey": null, "username": null, "name": null }, "ClientType": { "Other": null, "type": "System.GetUserList" }, "Content": null, "RecUserlist": null, "SendUser": null, "AddService": null };
}

var ReyCount = 0;
var websocket = {
    WebSocketOpen: function (username, open) {
        var wsImpl = window.WebSocket || window.MozWebSocket;
        window.ws = new wsImpl('ws://localhost:8181/username/' + username);
        window.ws.onopen  = function () {
            console.log("websocket ok");
            if (open != null) open();
        }
        window.ws.onclose = function (f) {
            
            
        }
        window.ws.onerror = function (d) {
            
        }
        window.ws.onmessage = function (data) {
            var json = JSON.parse(data.data);
    
            switch (json.Head.RequestKey) {
                case MessageType.uploadLocation: null; break;
                case MessageType.orderMessage: fun(json); break;
            }
        };
    },
    Send: function (str) {
        var msg = JSON.stringify(str);
        if (window.ws.readyState != 1) {
            ReyCount++;
            websocket.WebSocketOpen(userwxinfo.wxCount);
            if (ReyCount >= 5) clearInterval(stopConnetion);
        }
        window.ws.send(msg);
    },
    Struct:function () {
        return { "Head": { "KeyID": null, "RequestKey": null, "username": null, "name": null }, "ClientType": { "Other": null, "type": "System.GetUserList" }, "Content": null, "RecUserlist": null, "SendUser": null, "AddService": null };
    }
};


function fun(data)
{
    console.log(data.Content);
    var jsonorderinfo = data.Content;
    $.confirm('有用户需要从' + jsonorderinfo.startlocation + '到' + jsonorderinfo.endlocation + '费用:' + jsonorderinfo.driverprice+', 是否接单？', function () {
        ups(jsonorderinfo);
    });
    
    console.log(jsonorderinfo);
}

function ups(data) {
    var datas = { state: 4 };
    datas.driverid = window.top.databaseUserinfo.id;
    var userinfo = JSON.parse(data.ordersInfo);
    datas.driverInfo = JSON.stringify(window.top.driverInfo);
    var dt = userinfo.startDate;
    $.ajax({
        url: "/api/orders/meetOrder?id=" + data.id,
        type: "PUT",
        data: JSON.stringify(datas),
        contentType: "application/json",
        success: function (data) {
            if (data) {
                $.alert("接单成功。");
                var msg = "您从" + userinfo.startingPoint + "到" + userinfo.endingPoint + "的订单已经被接收。\r\n司机称呼：" + window.parent.driverInfo.usercall + "\r\n联系方式:" + window.parent.databaseUserinfo.mobile + "\r\n时间：" + dt;
                var msg2 = "您接收从" + userinfo.startingPoint + "到" + userinfo.endingPoint + "的订单已经被接收。\r\n用户称呼：" + userinfo.nickname + "\r\n联系方式:" + userinfo.rider + "\r\n时间：" + dt;
                SendMessage(msg, userinfo.openid);
                SendMessage(msg2, window.parent.databaseUserinfo.wxCount);
                window.location.reload();
            }
            else $.alert("订单已经被其他用户接单。");
        }
    });
}