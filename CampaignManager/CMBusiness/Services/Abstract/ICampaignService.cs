using CMEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMBusiness.Abstract
{
    public interface ICampaignService
    {
       List<CMEntities.Entities.Campaign> GetCampaignsForCustomerFromMailgun();
    }
}
