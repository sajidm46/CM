using CMEntities.Entities;
using MailgunAPIDirect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampaignManager.Models
{
    public class TagViewModel
    {
        public List<Tag> Tags { get; set; }
        //public Pager Pager { get; set; }
    }
}