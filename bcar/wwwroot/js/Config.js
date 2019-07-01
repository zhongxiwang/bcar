

var list = ["onMenuShareTimeline", "onMenuShareAppMessage", "onMenuShareQQ", "onMenuShareWeibo", "onMenuShareQZone",
    "chooseImage", "getNetworkType", "hideOptionMenu", "showOptionMenu", "hideMenuItems","uploadImage",
    "showMenuItems", "hideAllNonBaseMenuItem", "showAllNonBaseMenuItem", "closeWindow"
];
(function () {
    JsApiConfig();
})();

function JsApiConfig() {
    var xhr = new XMLHttpRequest();
    xhr.open("get", "/apiticket?url=" + window.location.href);
    xhr.onload = function (data) {
        var signature = data.currentTarget.responseText;
        wx.config({
            debug: false,
            appId: "wx6a2e25ee7abfca60",
            timestamp: "1414587457",
            nonceStr: "Wm3WZYTPz0wzccnW",
            signature: signature,
            jsApiList: list
        });
    }
    xhr.send();
}

//选择图片
function selectImage() {
    wx.chooseImage({
        count: 1, // 默认9
        sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
        sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
        success: function (res) {
            localIds = res.localIds;
        }
    });
}

function showImage() {
    wx.previewImage({
        current: '', // 当前显示图片的http链接
        urls: [] // 需要预览的图片http链接列表
    });
}
//本地图片展示
function localhostImage(id,ele) {
    wx.getLocalImgData({
        localId: id ,
        success: function (res) {
            var localData = res.localData;
            var img = document.createElement("img");
            img.src = localData;
            img.width = 160;
            ele.appendChild(img);
        }
    });
}
///开始上传图片
function uploadImage(id,ele) {
    wx.uploadImage({
        localId: id, 
        isShowProgressTips: 1,
        success: function (res) {
            var serverId = res.serverId;
            var ds = ele.getAttribute("id");
            driveri[ds] = serverId;
        }
    });
}

//上传图片组件
function uploadWxImage(inputid,imageid) {
    wx.chooseImage({
        count: 1, // 默认9
        sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
        sourceType: ['album', 'camera'], 
        success: function (res) {
            var tmp = res.localIds;
            var inputs = document.getElementById(inputid);
            var image = document.getElementById(imageid);
            localhostImage(tmp[0], image);
            uploadImage(tmp[0], inputs);
        }
    });
}