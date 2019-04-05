using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailgunAPIDirect.Entities
{
    public class Tag
    {
        [JsonProperty("tag")]
        public string tag { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("first-seen")]
        public DateTime first_seen { get; set; }
        [JsonProperty("last-seen")]
        public DateTime last_seen { get; set; }
    }
}
