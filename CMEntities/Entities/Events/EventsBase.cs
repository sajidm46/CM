using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMEntities.Entities.Events
{
    public class EventsBase
    {
        [JsonProperty("total")]
        public int total { get; set; }
    }
}
