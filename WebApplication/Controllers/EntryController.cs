using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication;
using Telerik.Web.Mvc.UI;
using Telerik.Web.Mvc.Extensions;


namespace WebApplication.Controllers
{ 
    public class EntryController : BaseController
    {
        //
        // GET: /Entry/

        [LoginAuthorize]
        public ViewResult Index()
        {
            var entries = db.Entries.Include("Creator");
            return View(entries.ToList());
        }

        //
        // GET: /Entry/Publish/5

        [LoginAuthorize]
        public ActionResult Publish(int id)
        {
            Entry entry = db.Entries.Single(e => e.Id == id);
            entry.Published = true;

            db.Entry(entry).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }

        //
        // GET: /Entry/UnPublish/5

        [LoginAuthorize]
        public ActionResult UnPublish(int id)
        {
            Entry entry = db.Entries.Single(e => e.Id == id);
            entry.Published = false;

            db.Entry(entry).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }

        //
        // GET: /Entry/Details/5

        [LoginAuthorize]
        public ViewResult Details(int id)
        {
            Entry entry = db.Entries.Single(e => e.Id == id);
            return View(entry);
        }

        //
        // GET: /Entry/Create
        [LoginAuthorize]
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.Select(e => e);
            ViewBag.SelectedCategories = new List<int>();
            ViewBag.enKeywords = db.Keywords.Where(e => e.Type == true);
            ViewBag.huKeywords = db.Keywords.Where(e => e.Type == false);
            return View();
        } 

        //
        // POST: /Entry/Create
        [LoginAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Entry entry, int[] selectedCategories, string huKeywords, string enKeywords)
        {
            KeywordsProcedure(entry, huKeywords, enKeywords);

            if (ModelState.IsValid)
            {
                entry.UserId = int.Parse((string)Session["UserId"]);
                entry.PublishedDate = DateTime.Now;
                entry.Categories.Clear();
                foreach (var id in selectedCategories.ToList())
                {
                    Category category = db.Categories.Single(e => e.Id == id);
                    entry.Categories.Add(category);
                }
                db.Entries.Add(entry);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.Categories = db.Categories.Select(e => e);
            ViewBag.SelectedCategories = selectedCategories.ToList();
            ViewBag.enKeywords = db.Keywords.Where(e => e.Type == true);
            ViewBag.huKeywords = db.Keywords.Where(e => e.Type == false);
            return View(entry);
        }

        
        //
        // GET: /Entry/Edit/5
        [LoginAuthorize]
        public ActionResult Edit(int id)
        {
            Entry entry = db.Entries.Single(e => e.Id == id);
            ViewBag.Categories = db.Categories.Select(e => e);
            ViewBag.SelectedCategories = entry.Categories.Select(e => e.Id).ToList<int>();
            ViewBag.enKeywords = db.Keywords.Where(e => e.Type == true);
            ViewBag.huKeywords = db.Keywords.Where(e => e.Type == false);
            return View(entry);
        }

        //
        // POST: /Entry/Edit/5
        [LoginAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Entry entry, int[] selectedCategories, string huKeywords, string enKeywords)
        {
            KeywordsProcedure(entry, huKeywords, enKeywords);

            if (ModelState.IsValid)
            {
                entry.UserId = int.Parse((string)Session["UserId"]);
                entry.Categories.Clear();
                foreach (var id in selectedCategories.ToList())
                {
                    Category category = db.Categories.Single(e => e.Id == id);
                    entry.Categories.Add(category);
                }
                
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.Select(e => e);
            ViewBag.SelectedCategories = selectedCategories.ToList();
            ViewBag.enKeywords = db.Keywords.Where(e => e.Type == true);
            ViewBag.huKeywords = db.Keywords.Where(e => e.Type == false);
            return View(entry);
        }

        //
        // GET: /Entry/Delete/5
        [LoginAuthorize]
        public ActionResult Delete(int id)
        {
            Entry entry = db.Entries.Single(e => e.Id == id);
            return View(entry);
        }

        //
        // POST: /Entry/Delete/5
        [LoginAuthorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Entry entry = db.Entries.Single(e => e.Id == id);
            db.Entries.Remove(entry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void KeywordsProcedure(Entry entry, string huKeywords, string enKeywords)
        {
            entry.Keywords.Clear();

            string[] separators = { ", " };
            var huKeywordsList = huKeywords.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
            var enKeywordsList = enKeywords.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var item in huKeywordsList)
            {
                var keyword = db.Keywords.SingleOrDefault(e => e.Value == item && !e.Type);
                if (keyword == null)
                    keyword = new Keyword { Type = false, Value = item };

                entry.Keywords.Add(keyword);
            }

            foreach (var item in enKeywordsList)
            {
                var keyword = db.Keywords.SingleOrDefault(e => e.Value == item && e.Type);
                if (keyword == null)
                    keyword = new Keyword { Type = true, Value = item };

                entry.Keywords.Add(keyword);
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}