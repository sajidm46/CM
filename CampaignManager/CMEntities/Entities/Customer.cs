using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMEntities.Entities
{
    [Table("Customer")]
    public class Customer 
    {
        public int id { get; set; }
        public string CustomerUniqueId { get; set; }
        public string DomainName { get; set; }
        public string ApiKey { get; set; }
        public string DBConnectionString { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FromAddress { get; set; }
        public DateTime lastupd { get; set; }
        public List<Campaign> Campaigns = new List<Campaign>();

        public Customer()
        {
            CustomerDetails theCustomerWeWant;
            string connString = ConfigurationManager.ConnectionStrings["CampaignManagerDB"].ToString();
            using (var connection = new SqlConnection(connString))
            { 
                theCustomerWeWant = connection.GetAll<CustomerDetails>().Where(c => c.LoginName == HttpContext.Current.User.Identity.Name).FirstOrDefault<CustomerDetails>(); ;
            }

            this.ApiKey = theCustomerWeWant.ApiKey;
            this.CustomerUniqueId = theCustomerWeWant.CustomerUniqueId;
            this.DBConnectionString = theCustomerWeWant.DBConnectionString;
            this.DomainName = theCustomerWeWant.DomainName;
            this.FromAddress = theCustomerWeWant.FromAddress;
            this.id = theCustomerWeWant.id;
            this.lastupd = theCustomerWeWant.lastupd;
            this.LoginName = theCustomerWeWant.LoginName;
            this.Password = theCustomerWeWant.Password;

            //this.Campaigns = GetCampaignsForCustomer();
        }

        //internal List<Campaign> GetCampaignsForCustomer()
        //{
        //    CampaignService campaignService = new CampaignService();
        //    listOfCampaigns = campaignService.GetCampaignsForCustomerFromMailgun();
        //}
    }
}
