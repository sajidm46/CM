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
        private List<Tag> listOfTagsFiltered;


        // GET: /Tag/
        [Authorize]
        [HttpGet]
        public ActionResult Index(string id, string tagFilter, string radioId= "")
        {
            List<Tag> listOfTags=new List<Tag>();
            
            TagService tagService = new TagService();
            if (Session["Tags"] == null)
            {
                listOfTags = tagService.Get();
                Session["Tags"] = listOfTags;
            }
            else
            {
                listOfTags = Session["Tags"] as List<Tag>;
            }
            listOfTagsFiltered = listOfTags;

            if (Session["radioId"] != null && radioId == "" )
            {
                radioId = Session["radioId"].ToString();
            }
            else
            {
                Session["radioId"] = radioId;
            }


            switch (radioId)
            {
                case "rd_last30": listOfTagsFiltered = listOfTags.Where(x => x.first_seen > DateTime.Now.AddDays(-30)).ToList();  break;
                case "rd_last365": listOfTagsFiltered = listOfTags.Where(x => x.first_seen > DateTime.Now.AddDays(-365)).ToList();  break;
                case "all": listOfTagsFiltered = listOfTags.ToList();  break;
            }
            listOfTagsFiltered = listOfTagsFiltered.Where(x => x.first_seen.ToShortDateString() != "01/01/0001").ToList();
            //foreach (Tag item in listOfTagsFiltered) //get fully tag stats for each tag... this will slow down whole website to load up....
            //{
            //   // EventStats viewModel = tagService.GetTagStats(item.tag, item.first_seen);
            //}


            ViewData["TotalTags"] = "Total Tags showing: " + listOfTagsFiltered.Count;
            if (listOfTagsFiltered.Count >= 1)
            {
                var viewModel = new TagViewModel
                {
                    Tags = listOfTagsFiltered
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
            List<Tag> listOfTags=new List<Tag>();
            if (User.Identity.Name != "CMAdmin@Ez-Runner.com")
            {
                TagService tagService = new TagService();
                if (Session["Tags"] == null)
                {
                    listOfTags = tagService.Get();
                    Session["Tags"] = listOfTags;
                }
                else
                {
                    listOfTags = Session["Tags"] as List<Tag>;
                }


                if (ViewData["tagService"] == null)
                {
                    listOfTags = tagService.Get();
                    ViewData["tagService"] = tagService;
                }
                else
                {
                    tagService = ViewData["tagService"] as TagService;
                }


                

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
        public ActionResult TagStats(string id,string start, string end)
        {
            
            start = "01/" + start.Substring(3, start.Length - 3);
            GetDaysInMonth(end, ref end);

            ViewData["TagID"] = id;
            DateTime thestartDateWeWant = Convert.ToDateTime(start);
            
            ViewData["startdate"] = start;
            ViewData["enddate"] = end;
            TagService tagService = new TagService();
            EventStats viewModel = tagService.GetTagStats(id, thestartDateWeWant);

            return View(viewModel);
        }

        private string GetDaysInMonth(string end, ref string lastDateofTheMonth)
        {
            string getDaysInAMonth = "31";
            DateTime dtDate = Convert.ToDateTime(end);
            switch (dtDate.Month)
            {
                case 4: case 6: case 9: case 11: getDaysInAMonth = "30"; break;
                case 2: if (dtDate.Year % 4 == 0) getDaysInAMonth = "29"; else getDaysInAMonth = "28"; break;
            }
            lastDateofTheMonth = getDaysInAMonth + dtDate.Month.ToString("/00/") + dtDate.Year.ToString();
            return getDaysInAMonth;
        }
    }
}