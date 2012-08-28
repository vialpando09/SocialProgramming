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
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;


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

            var attachments = TempData["Attachments"] as List<string>;
            var featuredImage = TempData["FeaturedImage"] as List<string>;

            if (ModelState.IsValid)
            {
                int userId = int.Parse((string)Session["UserId"]);
                entry.Creator = db.Users.Where(e => e.Id == userId).First();
                entry.PublishedDate = DateTime.Now;
                entry.Categories.Clear();
                foreach (var id in selectedCategories.ToList())
                {
                    Category category = db.Categories.Single(e => e.Id == id);
                    entry.Categories.Add(category);
                }
                if (attachments != null)
                {
                    foreach (var att in attachments)
                    {
                        WebApplication.File newAtt = new WebApplication.File { Entry = entry, Location = Guid.NewGuid().ToString(), Name = Path.GetFileName(att) };
                        db.Files.Add(newAtt);
                    }
                }
                if (featuredImage != null && featuredImage.Count > 0)
                    entry.FeaturedImage = featuredImage[0];
                else
                    entry.FeaturedImage = "";


                db.Entries.Add(entry);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException d)
                {

                    ViewBag.Categories = db.Categories.Select(e => e);
                    ViewBag.SelectedCategories = selectedCategories.ToList();
                    ViewBag.enKeywords = db.Keywords.Where(e => e.Type == true);
                    ViewBag.huKeywords = db.Keywords.Where(e => e.Type == false);
                    return View(entry);
                }
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
        public ActionResult Edit(Entry entry, int[] selectedCategories, string huKeywords, string enKeywords, string toDeleteFiles)
        {
            var toEditEntry = db.Entries.Where(e => e.Id == entry.Id).First();
            KeywordsProcedure(toEditEntry, huKeywords, enKeywords);

            var attachments = TempData["Attachments"] as List<string>;
            var featuredImage = TempData["FeaturedImage"] as List<string>;

            if (ModelState.IsValid)
            {
                toEditEntry.enContent = entry.enContent;
                toEditEntry.enIntroduction = entry.enIntroduction;
                toEditEntry.enTitle = entry.enTitle;
                toEditEntry.huContent = entry.huContent;
                toEditEntry.huIntroduction = entry.huIntroduction;
                toEditEntry.huTitle = entry.huTitle;
                toEditEntry.IsFeatured = entry.IsFeatured;
                toEditEntry.Keywords = entry.Keywords;
                toEditEntry.Published = entry.Published;
                toEditEntry.PublishedDate = entry.PublishedDate;                
                toEditEntry.UserId = int.Parse((string)Session["UserId"]);
                toEditEntry.Categories.Clear();
                foreach (var id in selectedCategories.ToList())
                {
                    Category category = db.Categories.Single(e => e.Id == id);
                    toEditEntry.Categories.Add(category);
                }
                //Delete to delete
                List<string> fileNames = new List<string>();
                foreach (var idString in toDeleteFiles.Split(',').Where(e => e != ""))
                {
                    int id = int.Parse(idString);
                    var file = db.Files.Where(e => e.Id == id).First();
                    fileNames.Add(file.Name);
                    toEditEntry.Files.Remove(file);
                    db.Files.Remove(file);
                }
                this.Delete("~/App_Data/Files", fileNames.ToArray());
                //Add new ones
                if (attachments != null)
                {
                    foreach (var att in attachments)
                    {
                        WebApplication.File newAtt = new WebApplication.File { Entry = toEditEntry, Location = Guid.NewGuid().ToString(), Name = Path.GetFileName(att) };
                        db.Files.Add(newAtt);
                    }
                }
                if (featuredImage != null && featuredImage.Count > 0)
                {
                    string[] fileName = { Path.GetFileName(entry.FeaturedImage) };
                    this.Delete("~/App_Data/Images", fileName);
                    toEditEntry.FeaturedImage = featuredImage[0];
                }
                else if (string.IsNullOrEmpty(entry.FeaturedImage))
                {
                    toEditEntry.FeaturedImage = "";
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                }
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
            var fileNames = entry.Files.Select(e => e.Name).ToArray();
            this.Delete("~/App_Data/Files", fileNames);
            foreach (var file in entry.Files.ToList())
            {
                db.Files.Remove(file);
            }
            if (!string.IsNullOrEmpty(entry.FeaturedImage))
            {
                string[] filenames = { Path.GetFileName(entry.FeaturedImage) };
                this.Delete("~/App_Data/Images", filenames);
            }
            var categories = entry.Categories.ToList();
            foreach (var category in categories)
            {
                category.Entries.Remove(entry);
            }
            var keywords = entry.Keywords.ToList();
            foreach (var keyword in keywords)
            {
                keyword.Entries.Remove(entry);
                if (keyword.Entries.Count == 0)
                    db.Keywords.Remove(keyword);
            }
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