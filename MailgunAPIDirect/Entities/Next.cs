using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MailgunAPIDirect.Entities
{
    public partial class Next
    {
        [JsonProperty("next")]
        public string NextNext { get; set; }
    }

    public partial class Next
    {
        public static Next FromJson(string json) => JsonConvert.DeserializeObject<Next>(json);
    }

}
