using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MailgunAPIDirect.Entities
{

    public partial class Event
    {
        [JsonProperty("geolocation")]
        public Geolocation Geolocation { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("mailing-list")]
        public MailingList MailingList { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("log-level")]
        public string LogLevel { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("campaigns")]
        public List<Campaign> Campaigns { get; set; }

        [JsonProperty("user-variables")]
        public UserVariables UserVariables { get; set; }

        [JsonProperty("recipient-domain")]
        public string RecipientDomain { get; set; }

        [JsonProperty("timestamp")]
        public double Timestamp { get; set; }

        [JsonProperty("client-info")]
        public ClientInfo ClientInfo { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        [JsonProperty("event")]
        public string EventEvent { get; set; }
    }

    public partial class Campaign
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class ClientInfo
    {
        [JsonProperty("client-os")]
        public string ClientOs { get; set; }

        [JsonProperty("device-type")]
        public string DeviceType { get; set; }

        [JsonProperty("client-name")]
        public string ClientName { get; set; }

        [JsonProperty("client-type")]
        public string ClientType { get; set; }

        [JsonProperty("user-agent")]
        public string UserAgent { get; set; }
    }

    public partial class Geolocation
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }

    public partial class MailingList
    {
        [JsonProperty("list-id")]
        public string ListId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("sid")]
        public string Sid { get; set; }
    }

    public partial class Message
    {
        [JsonProperty("headers")]
        public Headers Headers { get; set; }
    }

    public partial class Headers
    {
        [JsonProperty("message-id")]
        public string MessageId { get; set; }
    }

    public partial class UserVariables
    {
    }

    public partial class Event
    {
        public static List<Event> FromJson(string json) => JsonConvert.DeserializeObject<List<Event>>(json);
    }

    //public static class Serialize
    //{
    //    public static string ToJson(this List<Event> self) => JsonConvert.SerializeObject(self);
    //}

    //public class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //    };
    //}
}