using CMEntities.Entities;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MailgunAPIDirect.Entities;

namespace MailgunAPIDirect
{
    public class EventMethods : MailgunAPIDirectBase
    {
        public EventMethods()
            : base()
        {

        }

        public static List<Event> GetEvents()
        {
            List<Event> theEventsWeWant = new List<Event>();
            IList<Event> theEventsToAdd;
            customer = new Customer();
            
            var client = new RestClient
            {
                BaseUrl = _mailgunApiBaseUri,
                Authenticator = new HttpBasicAuthenticator("api", customer.ApiKey)
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                   customer.DomainName, ParameterType.UrlSegment);
            request.Resource = "{domain}/events";
            request.AddParameter("begin", "Wed, 4 February 2018 16:40:00 -0000");
            request.AddParameter("end", "Wed, 4 February 2018 16:30:00 -0000");
            request.AddParameter("ascending", "no");
            request.AddParameter("limit", 300);  //300 is the maximum amount you can retrieve at once.
            request.AddParameter("pretty", "yes");

            var response = client.Execute<dynamic>(request);
            var theEventsJson = JsonConvert.SerializeObject(response.Data["items"]);
            theEventsToAdd = JsonConvert.DeserializeObject<List<Event>>(theEventsJson);

            foreach (Event eventToAdd in theEventsToAdd)
            {
                theEventsWeWant.Add(eventToAdd);
            }

            //Repeat until no next page link adding all the events to one great big list of events.
            var next = JsonConvert.SerializeObject(response.Data["paging"]);
            Next nextPageOfResultsLink = JsonConvert.DeserializeObject<Next>(next);
            do
            {
                RestRequest request2 = new RestRequest(nextPageOfResultsLink.NextNext.ToString(), Method.GET);
                var response2 = client.Execute<dynamic>(request2);
                next = JsonConvert.SerializeObject(response2.Data["paging"]);
                nextPageOfResultsLink = JsonConvert.DeserializeObject<Next>(next);
                theEventsJson = JsonConvert.SerializeObject(response2.Data["items"]);
                theEventsToAdd = JsonConvert.DeserializeObject<List<Event>>(theEventsJson);
                foreach (Event eventToAdd in theEventsToAdd)
                {
                    theEventsWeWant.Add(eventToAdd);
                }

            } while (theEventsToAdd.Count != 0);

            
            return theEventsWeWant;
        }
    }
}
