using CMEntities.Entities;
using MailgunAPIDirect;
using MailgunAPIDirect.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CampaignManager.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        [HttpGet]
        [Route("event/{campaignId}")]
        [Authorize]
        public ActionResult Index()
        {
            List<Event> events = EventMethods.GetEvents();
            return View();
        }
    }
}