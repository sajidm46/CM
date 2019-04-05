using MailgunAPIDirect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMBusiness.Services.Abstract
{
    public interface ITagService
    {
        List<Tag> Get();
        EventStats GetTagStats(string id, DateTime startdate);
    }
}
