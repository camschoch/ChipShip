using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipShip.Models
{   
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var data = GettingStarted.FromJson(jsonString);
    //
    public partial class DistanceModel
    {
        [JsonProperty("origin_addresses")]
        public string[] OriginAddresses { get; set; }

        [JsonProperty("destination_addresses")]
        public string[] DestinationAddresses { get; set; }

        [JsonProperty("rows")]
        public Row[] Rows { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Row
    {
        [JsonProperty("elements")]
        public Element[] Elements { get; set; }
    }

    public partial class Element
    {
        [JsonProperty("duration")]
        public Distance Duration { get; set; }

        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Distance
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public partial class DistanceModel
    {
        public static DistanceModel FromJson(string json) => JsonConvert.DeserializeObject<DistanceModel>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DistanceModel self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

