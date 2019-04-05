using CMDataAccess.Repositories.Abstract;
using CMEntities.Entities;
using EZRunner.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDataAccess.Repositories.Concrete
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        private IEzLog _logger = EzLogger.Instance;
        internal static string DomainName = ConfigurationManager.AppSettings["DomainName"];

        public CampaignRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

    }
}
