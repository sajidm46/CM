using CMEntities.Entities;
using System;
using System.Configuration;

namespace MailgunAPIDirect
{
    public class MailgunAPIDirectBase
    {
        #region private
        private readonly string attachmentFileLocation = ConfigurationManager.AppSettings["ClientsFilePath"];
        public static readonly Uri _mailgunApiBaseUri = new Uri("https://api.mailgun.net/v3");
        public static Customer customer;
        #endregion

        public MailgunAPIDirectBase()
        {
            customer = new Customer();
        }
    }
    
}
