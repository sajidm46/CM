using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailgunAPIDirect.Entities
{
    public class EventSummary
    {
        public string tag;
        public long deliveredTotal = 0;
        public long acceptedTotal = 0;
        public long failedTotal = 0;
        public long openedTotal = 0;
        public long clickedTotal = 0;
        public long unsubscribedTotal = 0;
        public long complainedTotal = 0;
        public long stored = 0;
    }
}
