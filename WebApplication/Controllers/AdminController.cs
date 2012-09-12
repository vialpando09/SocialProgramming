using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

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

            List<DailyVisitors> dailyVisitors = new List<DailyVisitors>();
            List<MonthlyVisitors> monthlyVisitors = new List<MonthlyVisitors>();

            for(int i = -6; i <= 0; i++)
            {
                DateTime day = DateTime.Now.AddDays(i);
                dailyVisitors.Add(new DailyVisitors { Day = day.DayOfWeek.ToString(), Count = db.VisitorDataSet.Where(e => e.Date.Year == day.Year && e.Date.Month == day.Month && e.Date.Day == day.Day).Select(e => e.IpAddress).Distinct().Count() });
            }

            for (int i = -11; i <= 0; i++)
            {
                DateTime month = DateTime.Now.AddMonths(i);
                monthlyVisitors.Add(new MonthlyVisitors { Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Month).Substring(0, 3) + ".", Count = db.VisitorDataSet.Where(e => e.Date.Year == month.Year && e.Date.Month == month.Month).Select(e => e.IpAddress).Distinct().Count() });
            }

            ViewBag.DailyVisitors = dailyVisitors;
            ViewBag.MonthlyVisitors = monthlyVisitors;

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
