using CMEntities.Entities.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMEntities.Entities
{
    public class TagStats
    {
        [JsonProperty("time")]
        public string time { get; set; }

        [JsonProperty("accepted")]
        public IList<Accepted> accepted { get; set; }

        [JsonProperty("delivered")]
        public IList<Delivered> delivered { get; set; }

        [JsonProperty("failed")]
        public List<Failed> failed { get; set; }

        [JsonProperty("opened")]
        public IList<Opened> opened { get; set; }

        [JsonProperty("clicked")]
        public List<Clicked> clicked { get; set; }

        [JsonProperty("unsubscribed")]
        public List<Unsubscribed> unsubscribed { get; set; }

        [JsonProperty("complained")]
        public List<Complained> complained { get; set; }

        [JsonProperty("stored")]
        public List<Stored> stored { get; set; }
    }
}
