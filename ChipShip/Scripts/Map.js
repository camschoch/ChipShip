
setInterval(function () {
    function initMap() {
        var lat;
        var lng;
        //$.ajax({
        //    url: url,
        //    data: data,
        //    success: success,
        //});
          var uluru = {lat: -25.363, lng: 131.044};
          var map = new google.maps.Map(document.getElementById('map'), {
              zoom: 4,
              center: uluru
          });
          var marker = new google.maps.Marker({
              position: uluru,
              map: map
          });
    }
    getLocation()
}, 10000);
//var x = document.getElementById("map");
//function getLocation() {
//    if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(showPosition);
//    } else {
//        x.innerHTML = "Geolocation is not supported by this browser.";
//    }
//}
//function showPosition(position) {
//    x.innerHTML = "Latitude: " + position.coords.latitude + 
//    "<br>Longitude: " + position.coords.longitude;
//    lat = position.coords.latitude;
//    lng = position.coords.longitude;
//    alert(lat);
//    alert(lng);
//    //alert(x.innerHTML)
//}
