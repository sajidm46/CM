using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MailgunAPIDirect.Entities
{
    public partial class Delivered
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("delivered")]
        public DeliveredDelivered DeliveredDelivered { get; set; }
    }

    public partial class DeliveredDelivered
    {
        [JsonProperty("smtp")]
        public long Smtp { get; set; }

        [JsonProperty("http")]
        public long Http { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class Delivered
    {
        public static Delivered FromJson(string json) => JsonConvert.DeserializeObject<Delivered>(json, MailgunAPIDirect.Entities.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Delivered self) => JsonConvert.SerializeObject(self, MailgunAPIDirect.Entities.Converter.Settings);
    }

    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal,
                },
            },
        };
    }
}
