using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private const int TakeNumber = 5;

        [BasicAction]
        public ActionResult Entry(string date, int id, string title)
        {

            Entry entry;
            if (Session["UserType"] != null && (UserTypes)Session["UserType"] >= UserTypes.Administrator)
                entry= db.Entries.Single(e => e.Id == id); 
            else
                entry = db.Entries.Single(e => e.Id == id && e.Published); 
            if (entry == null)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.NoElement;
            }           
            return View(entry);
        }

        [BasicAction]
        public ActionResult Page(int id, string title)
        {
            Page page;
            if (Session["UserType"] != null && (UserTypes)Session["UserType"] >= UserTypes.Administrator)
                page = db.Pages.Single(e => e.Id == id);
            else
                page = db.Pages.Single(e => e.Id == id && e.Published);
            if (page == null)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.NoElement;
            }
            return View(page);
        }

        [BasicAction]
        public ActionResult Index()
        {
            var entries = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => e.IsFeatured && e.Published).Take(TakeNumber).Union(db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => !e.IsFeatured && e.Published).Take(TakeNumber));
            ViewBag.AjaxType = "AjaxLoadEntries";
            ViewBag.max = TakeNumber;
            return View(entries);
        }

        public ActionResult AjaxLoadEntries(string ids)
        {
            if (ids != "undefined")
            {
                List<string> idList = ids.Split(',').ToList();
                List<int> list = new List<int>();
                foreach (var id in idList)
                {
                    list.Add(int.Parse(id));
                }
                var model = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => !list.Contains(e.Id) && e.Published).Take(TakeNumber);
                ViewBag.ids = ids;
                ViewBag.max = TakeNumber;
                return View(model);
            }
            return Content("");
        }

        [BasicAction]
        public ActionResult Category(int id, string title)
        {
            var category = db.Categories.Single(e => e.Id == id);
            var entries = category.Entries.OrderByDescending(e => e.PublishedDate).Where(e => e.Published).Take(TakeNumber);
            
            if (entries.Count() == 0)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.NoElement;
            }
            
            ViewBag.Category = category;
            ViewBag.AjaxType = "AjaxLoadCategory";
            ViewBag.AjaxSecondParam = "&id=" + category.Id;
            ViewBag.max = TakeNumber;
            return View(entries);
        }

        public ActionResult AjaxLoadCategory(int id, string ids)
        {
            List<string> idList = ids.Split(',').ToList();
            List<int> list = new List<int>();
            foreach (var idL in idList)
            {
                list.Add(int.Parse(idL));
            }

            var category = db.Categories.Single(e => e.Id == id);
            var model = category.Entries.OrderByDescending(e => e.PublishedDate).Where(e => e.Published && !list.Contains(e.Id)).Take(TakeNumber);

            ViewBag.ids = ids;
            ViewBag.max = TakeNumber;
            return View(model);
        }

        [BasicAction]
        public ActionResult Tag(string tag)
        {
            ViewBag.Tag = tag;
            var entries = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => e.Published && e.Keywords.Where(f => f.Value.ToLower() == tag.ToLower()).Count() > 0).Take(TakeNumber);

            if (entries.Count() == 0)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.NoElement;
            }

            ViewBag.AjaxType = "AjaxLoadTag";
            ViewBag.AjaxSecondParam = "&tag=" + tag;
            ViewBag.max = TakeNumber;

            return View(entries);
        }

        public ActionResult AjaxLoadTag(string tag, string ids)
        {
            List<string> idList = ids.Split(',').ToList();
            List<int> list = new List<int>();
            foreach (var idL in idList)
            {
                list.Add(int.Parse(idL));
            }

            var model = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => !list.Contains(e.Id) && e.Published && e.Keywords.Where(f => f.Value.ToLower() == tag.ToLower()).Count() > 0).Take(TakeNumber);

            ViewBag.ids = ids;
            ViewBag.max = TakeNumber;
            return View(model);
        }

        [BasicAction]
        public ActionResult Search(string filter)
        {
            var filters = filter.Split(' ').ToList();
            var entries = new List<int>();
            foreach (var current in filters)
            {
                entries.AddRange(db.Entries.Where(e =>
                    !entries.Contains(e.Id) &&
                   (e.enContent.Contains(current) ||
                    e.huContent.Contains(current) ||
                    e.enIntroduction.Contains(current) ||
                    e.huIntroduction.Contains(current) ||
                    e.enTitle.Contains(current) ||
                    e.huTitle.Contains(current))).Select(e => e.Id).ToList());
            }

            var tags = db.Keywords.Where(e => filters.Contains(e.Value)).ToList();
            entries.AddRange(db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => !entries.Contains(e.Id) && e.Published && e.Keywords.Intersect(tags).Count() > 0).Select(e => e.Id).ToList());

            ViewBag.AjaxType = "AjaxLoadSearch";
            ViewBag.AjaxSecondParam = "&filter=" + filter;
            ViewBag.Filter = filter;
            ViewBag.max = TakeNumber;
            var model = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => entries.Contains(e.Id) && e.Published).Take(TakeNumber);

            if (model.Count() == 0)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.NoElement;
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(model);
        }

        public ActionResult AjaxLoadSearch(string filter, string ids)
        {
            List<string> idList = ids.Split(',').ToList();
            List<int> list = new List<int>();
            foreach (var id in idList)
            {
                list.Add(int.Parse(id));
            }

            var filters = filter.Split(' ').ToList();
            var entries = new List<int>();
            foreach (var current in filters)
            {
                entries.AddRange(db.Entries.Where(e =>
                    !entries.Contains(e.Id) &&
                   (e.enContent.Contains(current) ||
                    e.huContent.Contains(current) ||
                    e.enIntroduction.Contains(current) ||
                    e.huIntroduction.Contains(current) ||
                    e.enTitle.Contains(current) ||
                    e.huTitle.Contains(current))).Select(e => e.Id).ToList());
            }

            var tags = db.Keywords.Where(e => filters.Contains(e.Value)).ToList();
            entries.AddRange(db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => !entries.Contains(e.Id) && e.Published && e.Keywords.Intersect(tags).Count() > 0).Select(e => e.Id).ToList());

            var model = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => e.Published && entries.Contains(e.Id) && !list.Contains(e.Id)).Take(TakeNumber);
            ViewBag.ids = ids;
            ViewBag.max = TakeNumber;
            return View(model);
        }

        [BasicAction]
        public ActionResult Archive(int year, int month)
        {
            var entries = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => e.PublishedDate.Year == year && e.PublishedDate.Month == month && e.Published).Take(TakeNumber);

            if (entries.Count() == 0)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.NoElement;
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.AjaxType = "AjaxLoadArchive";
            ViewBag.AjaxSecondParam = "&year=" + year + "&month=" + month;
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.max = TakeNumber;
            return View(entries);
        }

        public ActionResult AjaxLoadArchive(int year, int month, string ids)
        {
            List<string> idList = ids.Split(',').ToList();
            List<int> list = new List<int>();
            foreach (var idL in idList)
            {
                list.Add(int.Parse(idL));
            }

            var model = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => !list.Contains(e.Id) && e.Published && e.PublishedDate.Year == year && e.PublishedDate.Month == month).Take(TakeNumber);

            ViewBag.ids = ids;
            ViewBag.max = TakeNumber;
            return View(model);
        }

        [BasicAction]
        public ActionResult ArchiveMore()
        {
            var entries = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => e.PublishedDate.Year < DateTime.Now.Year && e.Published).Take(TakeNumber);

            if (entries.Count() == 0)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.NoElement;
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.AjaxType = "AjaxLoadArchiveMore";
            ViewBag.max = TakeNumber;
            return View(entries);
        }

        public ActionResult AjaxLoadArchiveMore(string ids)
        {
            List<string> idList = ids.Split(',').ToList();
            List<int> list = new List<int>();
            foreach (var idL in idList)
            {
                list.Add(int.Parse(idL));
            }
            var model = db.Entries.Include("Categories").Include("Files").OrderByDescending(e => e.PublishedDate).Where(e => !list.Contains(e.Id) && e.Published && e.PublishedDate.Year < DateTime.Now.Year).Take(TakeNumber);

            ViewBag.ids = ids;
            ViewBag.max = TakeNumber;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var password = Common.CalculateMD5Hash(model.Password);
                var user = (from u in db.Users
                            where u.Username == model.UserName && u.Password == password
                            select u).FirstOrDefault();

                if (user != null)
                {
                    Session["UserType"] = (UserTypes)user.Type;
                    Session["UserId"] = user.Id.ToString();
                    Session["UserName"] = user.Username;

                    if (model.RememberMe)
                    {
                        var userData = Common.CalculateMD5Hash(DateTime.Now.ToString() + "VialpandoBlog" + "Na ezt találd ki hülye gyerek!");
                        string cookieName = "VialpandoBlogAuth";

                        HttpCookie myCookie = Request.Cookies[cookieName] ?? new HttpCookie(cookieName);
                        myCookie.Values["UserId"] = user.Id.ToString();
                        myCookie.Values["UserData"] = userData;
                        myCookie.Expires = DateTime.Now.AddDays(20);
                        Response.Cookies.Add(myCookie);

                        user.CookieHash = userData;
                        db.SaveChanges();
                    }

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.LoginMessage;
                    return Redirect(Request.UrlReferrer.ToString());
                }

                TempData["GlobalMessageType"] = MessageTypes.Error;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
                TempData["ViewBag.GlobalMessage"] = Resources.Admin.Login.Error;

            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [LoginAuthorize]
        public ActionResult Logout()
        {
            Session["UserType"] = null;
            Session["UserId"] = null;
            Session["UserName"] = null;

            string cookieName = "VialpandoBlogAuth";
                HttpCookie myCookie = Request.Cookies[cookieName];

                if (myCookie != null)
                {
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Set(myCookie);
                }

            TempData["GlobalMessageType"] = MessageTypes.Information;
            TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
            TempData["ViewBag.GlobalMessage"] = Resources.Common.LogoutMessage;

            return RedirectToAction("Index", "Home");
        }
    }
}
