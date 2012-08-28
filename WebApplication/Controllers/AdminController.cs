using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace WebApplication.Controllers
{
    public class AdminController : BaseController
    {
        private static RssModel GetRssFeed(string BlogUrl)
        {
            XDocument feedXml = XDocument.Load(BlogUrl);
            var title = from feed in feedXml.Descendants("channel")
                        select feed.Element("title").Value;

            var feeds = from feed in feedXml.Descendants("item")
                        select feed;

            var model = new RssModel { Elements = feeds, Title = title.First() };
            return model;
        }

        //
        // GET: /Admin/
        
        [LoginAuthorize]
        public ViewResult Index()
        {
            var count = db.Entries.Count();
            var countPerMonth = db.Entries.Count(e => e.PublishedDate.Month == DateTime.Now.Month);
            var newMessages = db.FeedBacks.Count(e => !e.Checked);

            ViewBag.RssFeed = GetRssFeed("http://msdn.microsoft.com/hu-hu/magazine/rss/default(en-us).aspx?z=z&iss=1");
            
            var model = new AdminModel { AllEntriesCount = count, AllEntriesCountInThisMonth = countPerMonth, NewMessagesCount = newMessages };
            return View(model);
        }

        [BasicAction]
        public ActionResult Login()
        {
            if (string.IsNullOrEmpty((string)Session["UserId"]))
                return View();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
