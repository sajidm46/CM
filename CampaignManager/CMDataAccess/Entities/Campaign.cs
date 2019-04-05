using Newtonsoft.Json;

namespace CMDataAccess.Entities
{
    public class Campaign
    {
        [JsonProperty("delivered_count")]
        public long delivered_count { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("created_at")]
        public string created_at { get; set; }
        [JsonProperty("clicked_count")]
        public long clicked_count { get; set; }
        [JsonProperty("opened_count")]
        public long opened_count { get; set; }
        [JsonProperty("submitted_count")]
        public long submitted_count { get; set; }
        [JsonProperty("unsubscribed_count")]
        public long unsubscribed_count { get; set; }
        [JsonProperty("bounced_count")]
        public long bounced_count { get; set; }
        [JsonProperty("complained_count")]
        public long complained_count { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty(" dropped_count")]
        public long dropped_count { get; set; }

        //List<Notification>[] Notifications{ get; set; }
    }
}
