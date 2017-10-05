using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models.ViewModels
{
    public class WalmartApiViewModel
    {
        public int numItems { get; set; }
        public int start { get; set; }
        public int totalResults { get; set; }
        public string query { get; set; }
        public string sort { get; set; }
        public List<ItemsItems>Items { get; set; }
    }
    public class ItemsItems
    {
        public int itemId { get; set; }
        public float salePrice { get; set; }
        public string name { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string thumbnailImage { get; set; }        
    }
    public partial class StoreLocator
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("no")]
        public long No { get; set; }

        [JsonProperty("stateProvCode")]
        public string StateProvCode { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("sundayOpen")]
        public bool SundayOpen { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }

    public partial class StoreLocator
    {
        public static StoreLocator[] FromJson(string json) => JsonConvert.DeserializeObject<StoreLocator[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this StoreLocator[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}