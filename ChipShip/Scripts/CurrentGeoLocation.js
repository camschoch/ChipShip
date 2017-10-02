$(document).ready(function () {
    console.log("ready!")

    
    setInterval(function () {
        navigator.geolocation.getCurrentPosition(function (position) {
            $.ajax({
                type: "POST",
                url: "UpToDateGeoLocation",
                data: {lat: position.coords.latitude , lng: position.coords.longitude},
                success: "GoodJob"
            });
        });
    }, 10000);
});