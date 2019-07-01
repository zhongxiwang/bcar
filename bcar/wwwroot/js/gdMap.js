function Init() {

}
window.onLoad = function () {


}

function marker(x, y) {
    return new AMap.Marker({
        position: new AMap.LngLat(116.39, 39.92),
        icon: '../image/car.png',
        offset: new AMap.Pixel(-13, -30)
    });
}