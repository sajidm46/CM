using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
namespace CMEntities.Entities
{
    [Table("CustomerDetails")]
    public class CustomerDetails 
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Please enter the CustomerUniqueId. E.G. FLI108")]
        public string CustomerUniqueId { get; set; }
        [Required(ErrorMessage = "Please enter the Domain Name")]
        public string DomainName { get; set; }
        [Required(ErrorMessage = "Please enter the Api Key")]
        public string ApiKey { get; set; }
        public string DBConnectionString { get; set; }
        [Required(ErrorMessage = "Please enter Login Name")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
        public string FromAddress { get; set; }
        public DateTime lastupd { get; set; }
    }

    
}
