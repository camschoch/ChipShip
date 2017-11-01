using ChipShip.Models;
using ChipShip.Models.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.StaticClasses
{
    static public class StaticClasses
    {
        static public List<ItemsItems> WalmartSearchApi(string parameter)
        {
            List<ItemsItems> searchResults = new List<ItemsItems>();
            var client = new RestClient("http://api.walmartlabs.com/v1/search?query=" + parameter + "&format=json&categoryId=976759&apiKey=njswjuajtb79zycw6ycpk7bq");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "bcb7837b-6825-07ff-46bb-390597ec652e");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic QUl6YVN5Q0N0X3RrOElzXzB3UnRmZkEzSDBZSFpFc184WndSTzNVOg==");
            IRestResponse<WalmartApiViewModel> response = client.Execute<WalmartApiViewModel>(request);
            foreach (var item in response.Data.Items)
            {
                searchResults.Add(item);
            }
            return searchResults;
        }
        static public List<string> GoogleGeoLocationApi(string address, int zip)
        {
            var formatted = address.Replace(" ", "+");
            List<string> location = new List<string>();
            var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/json?address=" + formatted + "+" + zip.ToString() + "&key=AIzaSyAxfTfQ9EoYEotKPAfWCncS40aCDuV88co");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "a37a2571-49a6-362e-a4b1-54e5ff540d63");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse<GeoLocationData> response = client.Execute<GeoLocationData>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var lattitude = response.Data.results[0].geometry[0].location[0].lat;
                var longitude = response.Data.results[0].geometry[0].location[0].lng;
                location.Add(lattitude.ToString());
                location.Add(longitude.ToString());
                return location;
            }
            return null;
        }
        static public StoreLocator WalmartLocatorApi(string city, string zip)
        {
            List<Array> test = new List<Array>();
            var client = new RestClient("http://api.walmartlabs.com/v1/stores?format=json&zip=" + zip + "&city=" + city + "&apiKey=njswjuajtb79zycw6ycpk7bq");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "ff62a73e-5529-e70f-d7d1-086f879646d9");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var convertedData = JsonConvert.DeserializeObject<List<StoreLocator>>(response.Content);
                return convertedData[0];            
            }
            return null;
        }
        //string userLocation, string walmoartLocation
        static public string GoogleDistanceApi(string userLat, string userLng, string walmartLat, string walmartLng)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" + userLat + "%2C" + userLng +"&destinations=" + walmartLat + "%2C" + walmartLng + "&key=AIzaSyAxfTfQ9EoYEotKPAfWCncS40aCDuV88co");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "619ff567-af23-8b90-6b95-946348249978");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var convertedData = JsonConvert.DeserializeObject<DistanceModel>(response.Content);
                return convertedData.Rows[0].Elements[0].Distance.Text;
            }
            return null;
        }
    }
}