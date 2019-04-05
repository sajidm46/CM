using EZNotificationsEntities;
using MailgunDirect45NetFramework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNotificationsDataAccess.Repositories.Abstract
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        IEnumerable<Notification> GetNotificationsForCustomer(string uniquecustomerid);
        IEnumerable<Notification> GetNotificationsForMessage(string uniquecustomerid, string messageid);
        IEnumerable<Notification> GetNotificationsForCampaign(string campaignid);
    }
}
