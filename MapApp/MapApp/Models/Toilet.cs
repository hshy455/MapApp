using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms.Maps;

namespace MapApp.Models
{
    public partial class Toilet
    {
        [JsonProperty("districtID")]
        public string DistrictId { get; set; }

        [JsonProperty("map_type")]
        public string MapType { get; set; }

        [JsonProperty("name_e")]
        public string NameE { get; set; }

        [JsonProperty("address_e")]
        public string AddressE { get; set; }

        [JsonProperty("contact1")]
        public string Contact1 { get; set; }

        [JsonProperty("contact2")]
        public string Contact2 { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("openHr_e")]
        public string OpenHrE { get; set; }

        [JsonProperty("map_coordinate")]
        public string MapCoordinate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Position Position { get; set; }

        [JsonProperty("updateDate")]
        public DateTimeOffset UpdateDate { get; set; }

        [JsonProperty("remarks_e")]
        public string RemarksE { get; set; }
    }

    public partial class Toilet
    {
        public static Dictionary<string, Toilet> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Toilet>>(json, MapApp.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, Toilet> self) => JsonConvert.SerializeObject(self, MapApp.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
