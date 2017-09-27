using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.StaticClasses
{
    static public class StaticClasses
    {
        static public void WalmartApi()
        {
            var client = new RestClient("http://api.walmartlabs.com/v1/search?query=food&format=json&apiKey=njswjuajtb79zycw6ycpk7bq");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "a61605ea-02de-dba8-b72d-705882b7b893");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic QUl6YVN5Q0N0X3RrOElzXzB3UnRmZkEzSDBZSFpFc184WndSTzNVOg==");
            IRestResponse response = client.Execute(request);
            
        }
    }
}