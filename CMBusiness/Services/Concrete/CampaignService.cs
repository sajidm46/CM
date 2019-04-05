using CMBusiness.Abstract;
using CMEntities.Entities;
using MailgunAPIDirect;
using System.Collections.Generic;

namespace CMBusiness.Concrete
{
    public class CampaignService : ICampaignService
    {
        public CampaignService()
        {
        }

        public List<CMEntities.Entities.Campaign> GetCampaignsForCustomerFromMailgun()
        {
            List<CMEntities.Entities.Campaign> listOfCampaigns = new List<CMEntities.Entities.Campaign>();
            listOfCampaigns =  CampaignMethods.GetCampaigns();
            return listOfCampaigns;
        }

        //public List<Tag> GetTagsForCustomerFromMailgun()
        //{
        //    List<Tag> listOfTags = new List<Tag>();
        //    listOfTags = CampaignMethods.GetTags();
        //    return listOfTags;
        //}
    }
}
