using CMEntities.Entities;
using MailgunAPIDirect.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using SimpleJson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailgunAPIDirect
{
    public class TagMethods : MailgunAPIDirectBase
    {
        public TagMethods()
            : base()
        {

        }

        public static Tag GetTag(string id)
        {
            //--------------------
            var restClient = new RestClient("http://localhost");
            var req = new RestRequest("bob", Method.GET);
            //------------------------------------


            customer = new Customer();

            var client = new RestClient
            {
                BaseUrl = _mailgunApiBaseUri,
                Authenticator = new HttpBasicAuthenticator("api", customer.ApiKey)
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                   customer.DomainName, ParameterType.UrlSegment);
            request.Resource = "{domain}/tags/{tag}";
            request.AddUrlSegment("tag", id);

            var response = client.Execute<dynamic>(request);
            Tag theTagWeWant = JsonConvert.DeserializeObject<Tag>(response.Content);

            return theTagWeWant;
        }

        public static List<Tag> GetTags()
        {
            List<Tag> theTagsWeWant = new List<Tag>();
            IList<Tag> theTagsToAdd;
            customer = new Customer();
            //customer.ApiKey = "key-44564bb97c8b3db9d24a2bb81713fe84"; sm?
              var client = new RestClient
            {
                BaseUrl = _mailgunApiBaseUri,
                Authenticator = new HttpBasicAuthenticator("api", customer.ApiKey)
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                   customer.DomainName, ParameterType.UrlSegment);
            request.Resource = "{domain}/tags";
            request.AddParameter("limit", 1001);
            //request.AddParameter("page", "Next");

            var response = client.Execute<dynamic>(request);
            if (response.Data["items"] != null)
            {
                var theTagsJson = JsonConvert.SerializeObject(response.Data["items"]);
                theTagsToAdd = JsonConvert.DeserializeObject<List<Tag>>(theTagsJson);

                foreach (Tag tagToAdd in theTagsToAdd)
                {
                    theTagsWeWant.Add(tagToAdd);
                }

                //Repeat until no tags to add
                var next = JsonConvert.SerializeObject(response.Data["paging"]);
                string strnext = next;

                //strnext = strnext.Replace(strnext.Substring(strnext.IndexOf("&tag=_cmp__platinum_price_for_gold_acton"), "&tag=_cmp__platinum_price_for_gold_acton".Length), "");
                strnext = strnext.Replace("http://", "https://");
                Next nextPageOfResultsLink = JsonConvert.DeserializeObject<Next>(strnext);
                //http://api.mailgun.net/v3/oxygenfreejumping.co.uk/tags?page=next&tag=_cmp__platinum_price_for_gold_acton&limit=1000
                //http://api.mailgun.net/v3/oxygenfreejumping.co.uk/tags?page=next&tag=_cmp__platinum_price_for_gold_acton&limit=1000
                //theTagsToAdd = null;
                try
                {
                    if (theTagsToAdd.Count == 1000)
                    {
                        do
                        {
                            RestRequest request2 = new RestRequest(nextPageOfResultsLink.NextNext.ToString(), Method.GET);
                            var response2 = client.Execute<dynamic>(request2);
                           
                                next = JsonConvert.SerializeObject(response2.Data["paging"]);
                            strnext = next;
                                strnext = strnext.Replace("http://", "https://");
                                nextPageOfResultsLink = JsonConvert.DeserializeObject<Next>(strnext);
                                theTagsJson = JsonConvert.SerializeObject(response2.Data["items"]);
                                theTagsToAdd = JsonConvert.DeserializeObject<List<Tag>>(theTagsJson);
                                foreach (Tag tagToAdd in theTagsToAdd)
                                {
                                    theTagsWeWant.Add(tagToAdd);
                                }
                           

                        } while (theTagsToAdd.Count == 1000);
                    }
                }
                catch (Exception ex)
                {
                }
                var tagsPurged = theTagsWeWant.Where(t => t.tag.Contains("[CMP]"));
                var delievered = theTagsWeWant.Where(dt => dt.first_seen != Convert.ToDateTime("01/01/0001")).OrderBy(d => d.first_seen).First();
                return tagsPurged.OrderByDescending(t => t.first_seen).ToList<Tag>();
            }

            return null;
        }

        

        public static IRestResponse DeleteTag(string id)
        {
            customer = new Customer();

            var client = new RestClient
            {
                BaseUrl = _mailgunApiBaseUri,
                Authenticator = new HttpBasicAuthenticator("api", customer.ApiKey)
            };
            RestRequest request = new RestRequest();
            request.Method = Method.DELETE;
            request.AddParameter("domain",
                                   customer.DomainName, ParameterType.UrlSegment);
            request.Resource = "{domain}/tags/{tag}";
            request.AddUrlSegment("tag", id);

            var response = client.Execute(request);

            return client.Execute(request);
        }

        public static List<EventStats> GetTagStats(string id,DateTime startdate)
        {
            long startDate = ToUnixTime(startdate);

            customer = new Customer();

            var client = new RestClient
            {
                BaseUrl = _mailgunApiBaseUri,
                Authenticator = new HttpBasicAuthenticator("api", customer.ApiKey)
            };
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                   customer.DomainName, ParameterType.UrlSegment);
            request.Resource = "{domain}/tags/{tag}/stats";
            request.AddUrlSegment("tag", id);
            //request.AddUrlSegment("tag", testTag);
            request.AddParameter("event", "delivered");
            request.AddParameter("event", "accepted");
            request.AddParameter("event", "failed");
            request.AddParameter("event", "opened");
            request.AddParameter("event", "clicked");
            request.AddParameter("event", "unsubscribed");
            request.AddParameter("event", "complained");

            request.AddParameter("event", "stored");
            //request.AddParameter("event", "temporary fail");
            //request.AddParameter("event", "permanent fail");
            request.AddParameter("start", startDate);

            var response =client.Execute<dynamic>(request);
            try
            {
                var json = JsonConvert.SerializeObject(response.Data["stats"]);

                List<EventStats> eventStats = JsonConvert.DeserializeObject<List<EventStats>>(json);
                eventStats.ForEach(es => es.tag = id);

                return eventStats;
            }
            catch (Exception)
            {

                return null;
            }
            

        }

        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }

    }
}
