namespace MailgunAPIDirect.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial  class EventStats
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("tag")]
        public string tag { get; set; }

        [JsonProperty("accepted")]
        public Accepted Accepted { get; set; } = new Accepted();

        [JsonProperty("delivered")]
        public Delivered Delivered { get; set; } = new Delivered();

        [JsonProperty("failed")]
        public Failed Failed { get; set; } = new Failed();

        [JsonProperty("stored")]
        public Complained Stored { get; set; } = new Complained();

        [JsonProperty("opened")]
        public Clicked Opened { get; set; } = new Clicked();

        [JsonProperty("clicked")]
        public Clicked Clicked { get; set; } = new Clicked();

        [JsonProperty("unsubscribed")]
        public Complained Unsubscribed { get; set; } = new Complained();

        [JsonProperty("complained")]
        public Complained Complained { get; set; } = new Complained();
    }

    public partial class Accepted
    {
        [JsonProperty("incoming")]
        public long Incoming { get; set; } = 0;

        [JsonProperty("outgoing")]
        public long Outgoing { get; set; } = 0;

        [JsonProperty("total")]
        public long Total { get; set; } = 0;
    }

    public partial class Clicked
    {
        [JsonProperty("total")]
        public long Total { get; set; } = 0;

        [JsonProperty("unique")]
        public long Unique { get; set; } = 0;
    }

    public partial class Complained
    {
        [JsonProperty("total")]
        public long Total { get; set; } = 0;
    }

    public partial class Delivered
    {
        [JsonProperty("smtp")]
        public long Smtp { get; set; } = 0;

        [JsonProperty("http")]
        public long Http { get; set; } = 0;

        [JsonProperty("total")]
        public long Total { get; set; } = 0;
    }

    public partial class Failed
    {
        [JsonProperty("temporary")]
        public Temporary Temporary { get; set; } = new Temporary();

        [JsonProperty("permanent")]
        public Permanent Permanent { get; set; } = new Permanent();
    }

    public partial class Permanent
    {
        [JsonProperty("suppress-bounce")]
        public long SuppressBounce { get; set; } = 0;

        [JsonProperty("suppress-unsubscribe")]
        public long SuppressUnsubscribe { get; set; } = 0;

        [JsonProperty("suppress-complaint")]
        public long SuppressComplaint { get; set; } = 0;

        [JsonProperty("bounce")]
        public long Bounce { get; set; } = 0;

        [JsonProperty("delayed-bounce")]
        public long DelayedBounce { get; set; } = 0;

        [JsonProperty("total")]
        public long Total { get; set; } = 0;
    }

    public partial class Temporary
    {
        [JsonProperty("espblock")]
        public long Espblock { get; set; } = 0;

        [JsonProperty("total")]
        public long Total { get; set; } = 0;
    }

    public partial class EventStats
    {
        public static EventStats[] FromJson(string json) => JsonConvert.DeserializeObject<EventStats[]>(json, MailgunAPIDirect.Entities.Converter.Settings);
    }

}
