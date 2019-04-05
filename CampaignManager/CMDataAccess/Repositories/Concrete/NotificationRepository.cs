using Dapper.Contrib.Extensions;
using EZNotificationsDataAccess.Repositories.Abstract;
using EZNotificationsEntities;
using EZRunner.Logger;
using MailgunDirect45NetFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RestSharp;

namespace EZNotificationsDataAccess.Repositories.Concrete
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private IEzLog _logger = EzLogger.Instance;
        internal static string DomainName = ConfigurationManager.AppSettings["DomainName"];

        public NotificationRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public IEnumerable<Notification> GetNotificationsForCustomer(string uniquecustomerid)
        {
            List<Notification> notifications = null;
            _logger.LogError("About to connect");

            using (var conn = Connection)
            {
                _logger.LogError("In Connection Loop");
                notifications = SqlMapperExtensions.GetAll<Notification>(conn, Transaction).Where(cuid => cuid.CustomerUniqueId == uniquecustomerid && cuid.ClientNotified == false).Take<Notification>(1000).ToList<Notification>();
                _logger.LogError("Got the notifications");
                foreach (var notification in notifications)
                {
                    _logger.LogError("updating notifications for" + notification.CustomerUniqueId);
                    notification.ClientNotified = true;
                    conn.Update(notification, Transaction);
                }
                Transaction.Commit();
            }
            return notifications;
        }

        public IEnumerable<Notification> GetNotificationsForMessage(string uniquecustomerid, string messageid)
        {
            List<Notification> notifications = null;
            using (var conn = Connection)
            {
                notifications = SqlMapperExtensions.GetAll<Notification>(conn, Transaction).Where(cuid => cuid.CustomerUniqueId == uniquecustomerid && cuid.MessageName == messageid).Take<Notification>(1000).ToList<Notification>();
            }
            return notifications;
        }

        public IEnumerable<Notification> GetNotificationsForCampaign(string campaignid)
        {
            IEnumerable<Notification> notifications = null;
            notifications =(IEnumerable<Notification>)MailgunApiDirect.GetEventsForCampaign(DomainName,campaignid);
            return notifications;
        }

    }
}
