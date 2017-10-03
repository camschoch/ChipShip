using ChipShip.Models;
using ChipShip.Models.ViewModels;
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
        static public void WalmartItemIdApi(string parameter)
        {
            var client = new RestClient("http://api.walmartlabs.com/v1/items/" + parameter + "?format=json&apiKey=njswjuajtb79zycw6ycpk7bq");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "82b00ac9-4e9f-ef47-1e03-f9028d5e3c4e");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse<ItemsItems> response = client.Execute<ItemsItems>(request);

        }
        static public List<string> GoogleGeoLocationApi(string address, int zip, string userId)
        {
            var formatted = address.Replace(" ", "+");
            List<string> location = new List<string>();
            //1600%20Amphitheatre%20Parkway%2C%20Mountain%20View%2C%20CA
            //address = "1600+Amphitheatre+Parkway+Mountain+View";
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
    }
}