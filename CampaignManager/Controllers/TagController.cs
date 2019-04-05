using CampaignManager.Models;

using CMBusiness.Services.Concrete;
using MailgunAPIDirect;
using MailgunAPIDirect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CampaignManager.Controllers
{
    public class TagController : Controller
    {
        // GET: /Tag/
        [Authorize]
        public ActionResult Index(string id)
        {
            List<Tag> listOfTags;
            
            TagService tagService = new TagService();
            listOfTags = tagService.Get();
            if (listOfTags.Count >= 1)
            {
                var viewModel = new TagViewModel
                {
                    Tags = listOfTags

                };
                return View(viewModel);
            }
            else { ViewBag.Message = "No Campaigns Found."; }
                
            return View();
        }

        // POST: /Tag/Delete/
        [Authorize]
        public ActionResult Delete()
        {
            List<Tag> listOfTags;
            if (User.Identity.Name != "CMAdmin@Ez-Runner.com")
            {
                TagService tagService = new TagService();
                listOfTags = tagService.Get();

                var viewModel = new TagViewModel
                {
                    Tags = listOfTags

                };
                return View(viewModel);
            }
            return View();
        }

        // POST: /Tag/DeleteTag/?tagID=thetagtodelete
        [Authorize]
        public ActionResult DeleteTag(string id)
        {
            TagService tagService = new TagService();
            Tag theTagWeWant = tagService.Get(id);
            //Call MailgunAPI delete tag method;
            string reponseMessage = tagService.Delete(id);
            return RedirectToAction("Delete");
        }

        // POST: /Tag/GetTagStats/?tagID=theTagId
        [Authorize]
        public ActionResult TagStats(string id,string start)
        {
            string tagName = "";
            DateTime thestartDateWeWant = Convert.ToDateTime(start);
            TagService tagService = new TagService();
            EventStats viewModel = tagService.GetTagStats(id, thestartDateWeWant);

            return View(viewModel);
        }
    }
}