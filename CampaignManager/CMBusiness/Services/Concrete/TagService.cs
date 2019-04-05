using CMBusiness.Services.Abstract;
using MailgunAPIDirect;
using MailgunAPIDirect.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMBusiness.Services.Concrete
{
    public class TagService : ITagService
    {
        public TagService()
        {
        }

        public List<Tag> Get()
        {
            List<Tag> listOfTags = new List<Tag>();
            try
            {
                listOfTags = TagMethods.GetTags();
            }
            catch (Exception e)
            {

               
            }
            
            return listOfTags;
        }

        public Tag Get(string id)
        {
            Tag theTagWeWant = TagMethods.GetTag(id);
            return theTagWeWant;
        }

        public string Delete(string id)
        {
            IRestResponse response = TagMethods.DeleteTag(id);
            if (!response.IsSuccessful)
            {
                return "Error deleting";
            }
            return "Tag deleted successfully";
        }

        public EventStats GetTagStats(string id, DateTime startdate)
        {
           List<EventStats> eventStats = TagMethods.GetTagStats(id,startdate);
           return GetStatsCumulativeTotals(eventStats);
        }

        internal EventStats GetStatsCumulativeTotals(List<EventStats> listOfStatsByDay)
        {
            EventStats theEventStatsWeWant = new EventStats();

            foreach (EventStats eventStat in listOfStatsByDay)
            {
                theEventStatsWeWant.tag = eventStat.tag;
                theEventStatsWeWant.Accepted.Incoming = theEventStatsWeWant.Accepted.Incoming + eventStat.Accepted.Incoming;
                theEventStatsWeWant.Accepted.Outgoing = theEventStatsWeWant.Accepted.Outgoing + eventStat.Accepted.Outgoing;
                theEventStatsWeWant.Accepted.Total = theEventStatsWeWant.Accepted.Total + eventStat.Accepted.Total;
                theEventStatsWeWant.Clicked.Unique = theEventStatsWeWant.Clicked.Unique + eventStat.Clicked.Unique;
                theEventStatsWeWant.Clicked.Total = theEventStatsWeWant.Clicked.Total + eventStat.Clicked.Total;
                theEventStatsWeWant.Complained.Total = theEventStatsWeWant.Complained.Total + eventStat.Complained.Total;
                theEventStatsWeWant.Delivered.Http = theEventStatsWeWant.Delivered.Http + eventStat.Delivered.Http;
                theEventStatsWeWant.Delivered.Smtp = theEventStatsWeWant.Delivered.Smtp + eventStat.Delivered.Smtp;
                theEventStatsWeWant.Delivered.Total = theEventStatsWeWant.Delivered.Total + eventStat.Delivered.Total;
                theEventStatsWeWant.Failed.Permanent.Bounce = theEventStatsWeWant.Failed.Permanent.Bounce + eventStat.Failed.Permanent.Bounce;
                theEventStatsWeWant.Failed.Permanent.DelayedBounce = theEventStatsWeWant.Failed.Permanent.DelayedBounce + eventStat.Failed.Permanent.DelayedBounce;
                theEventStatsWeWant.Failed.Permanent.SuppressBounce = theEventStatsWeWant.Failed.Permanent.SuppressBounce + eventStat.Failed.Permanent.SuppressBounce;
                theEventStatsWeWant.Failed.Permanent.SuppressComplaint = theEventStatsWeWant.Failed.Permanent.SuppressComplaint + eventStat.Failed.Permanent.SuppressComplaint;
                theEventStatsWeWant.Failed.Permanent.SuppressUnsubscribe = theEventStatsWeWant.Failed.Permanent.SuppressUnsubscribe + eventStat.Failed.Permanent.SuppressUnsubscribe;
                theEventStatsWeWant.Failed.Permanent.Total = theEventStatsWeWant.Failed.Permanent.Total + eventStat.Failed.Permanent.Total;
                theEventStatsWeWant.Failed.Temporary.Espblock = theEventStatsWeWant.Failed.Temporary.Espblock + eventStat.Failed.Temporary.Espblock;
                theEventStatsWeWant.Failed.Temporary.Total = theEventStatsWeWant.Failed.Temporary.Total + eventStat.Failed.Temporary.Total;
                theEventStatsWeWant.Opened.Unique = theEventStatsWeWant.Opened.Unique + eventStat.Opened.Unique;
                theEventStatsWeWant.Opened.Total = theEventStatsWeWant.Opened.Total + eventStat.Opened.Total;
                theEventStatsWeWant.Stored.Total = theEventStatsWeWant.Stored.Total = eventStat.Stored.Total;
                theEventStatsWeWant.Unsubscribed.Total = theEventStatsWeWant.Unsubscribed.Total + eventStat.Unsubscribed.Total;
            }

            return theEventStatsWeWant;
        }
    }
}
