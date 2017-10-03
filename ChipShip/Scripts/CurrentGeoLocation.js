var lat;
var lng;
$(document).ready(function () {
    console.log("ready!")
    

    setInterval(function () {
        getLocation()
        //alert(x.innerHTML)
        //alert(lat);
        //alert(lng);
        if (lat != undefined && lng != undefined) {
            $.ajax({
                type: "POST",
                url: "UpToDateGeoLocation",
                data: { lat: lat, lng: lng },
                success: "GoodJob"
            });
        }
    }, 10000);

    var x = document.getElementById("map");
    function getLocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            x.innerHTML = "Geolocation is not supported by this browser.";
        }
    }
    function showPosition(position) {
        //x.innerHTML = "Latitude: " + position.coords.latitude +
        //"<br>Longitude: " + position.coords.longitude;
        lat = position.coords.latitude;
        lng = position.coords.longitude;       
    }
});
//Latitude: 43.0341127<br>Longitude: -87.9117843