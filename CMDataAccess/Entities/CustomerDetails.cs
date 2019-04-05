using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDataAccess.Entities
{
    [Table("CustomerDetails")]
    public class CustomerDetails
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
    }
}
