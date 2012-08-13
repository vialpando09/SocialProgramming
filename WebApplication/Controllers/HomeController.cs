using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class HomeController : BaseController
    {    
        [BasicAction]
        public ActionResult Index()
        {
            var entries = db.Entries.Include("Creator").Where(e => e.Published);            
            return View(entries);
        }

        [BasicAction]
        public ActionResult Entry(string date, int id, string title)
        {
            var entry = db.Entries.Single(e => e.Id == id);
            return View(entry);
        }

        [BasicAction]
        public ActionResult Category(int id, string title)
        {
            var entries = db.Entries.Where(e => e.Published && e.Categories.Where(f => f.Id == id).Count() != 0 );

            ViewBag.Category = db.Categories.Single(e => e.Id == id);

            return View(entries);
        }

        [BasicAction]
        public ActionResult Tag(string tag)
        {
            ViewBag.Tag = tag;
            var entries = db.Entries.Where(e => e.Published && e.Keywords.Where(f => f.Value.ToLower() == tag.ToLower()).Count() > 0);

            return View(entries);
        }

        [BasicAction]
        public ActionResult Search(string filter)
        {
            var filters = filter.Split(' ').ToList();
            var entries = new List<Entry>();
            Parallel.ForEach(filters, current =>
            {
                entries.AddRange(db.Entries.Where(e =>
                    !entries.Contains(e) &&
                   (e.enContent.Contains(current) ||
                    e.huContent.Contains(current) ||
                    e.enIntroduction.Contains(current) ||
                    e.huIntroduction.Contains(current) ||
                    e.enTitle.Contains(current) ||
                    e.huTitle.Contains(current))));
            });

            var tags = db.Keywords.Where(e => filters.Contains(e.Value)).ToList();
            entries.AddRange(db.Entries.Where(e => !entries.Contains(e) && e.Keywords.Intersect(tags).Count() > 0));

            return View(entries);
        }

        [BasicAction]
        public ActionResult Archive(int year, int month)
        {
            var entries = db.Entries.Where(e => e.PublishedDate.Year == year && e.PublishedDate.Month == month);
            return View(entries);
        }

        [BasicAction]
        public ActionResult Archive(string more)
        {
            if (more == "More")
            {
                var entries = db.Entries.Where(e => e.PublishedDate.Year < DateTime.Now.Year);
                return View(entries);
            }
            return RedirectToAction("Index");
        }
    }
}
