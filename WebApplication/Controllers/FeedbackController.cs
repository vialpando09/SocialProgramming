using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using CaptchaSupport;

namespace WebApplication.Controllers
{
    public class FeedbackController : Controller, IDisposable
    {
        //
        // GET: /Feedback/

        private readonly ModelContainer db = new ModelContainer();

        [LoginAuthorize]
        public ActionResult Index()
        {
            var feedback = from fb in db.FeedBacks
                           select fb;
            return View(feedback);
        }

        public ActionResult Send()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Send(FeedbackModel model)
        {
            if (ModelState.IsValid)
            {
                if ((string)Session["CaptchaString"] != model.CaptchaText)
                {
                    ModelState.AddModelError("CaptchaError", Resources.Feedback.Index.CaptchaError);
                    model.CaptchaText = "";
                    return View(model);
                }

                db.FeedBacks.AddObject(new FeedBack { Checked = false, EmailAddress = model.EmailAddress, Message = model.Message, SendDate = DateTime.Now });
                db.SaveChanges();

                ViewBag.GlobalMessage = Resources.Feedback.Index.Success;

                return RedirectToAction("Index", "Home");

            }
            model.CaptchaText = "";
            return View(model);
        }

        [LoginAuthorize]
        public ActionResult MarkAsRead(int id)
        {
            var element = db.FeedBacks.Single(e => e.Id == id);

            element.Checked = true;
            db.SaveChanges();

            return RedirectToAction("Index", "Feedback");
        }

        [LoginAuthorize]
        public ActionResult MarkUnRead(int id)
        {
            var element = db.FeedBacks.Single(e => e.Id == id);

            element.Checked = false;
            db.SaveChanges();

            return RedirectToAction("Index", "Feedback");
        }

        [LoginAuthorize]
        public ViewResult Details(int id)
        {
            var element = db.FeedBacks.Single(e => e.Id == id);

            element.Checked = true;
            db.SaveChanges();

            return View(element);
        }
        
        [LoginAuthorize]
        public ActionResult Delete(int id)
        {
            var element = db.FeedBacks.Single(e => e.Id == id);
            
            return RedirectToAction("Index", "Admin");
        }

        [LoginAuthorize]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var element = db.FeedBacks.Single(e => e.Id == id);

            db.DeleteObject(element);
            db.SaveChanges();

            ViewBag.GlobalMessage = Resources.Common.Success;
            ViewBag.GlobalHeader = Resources.Common.Information;

            return RedirectToAction("Index", "Admin");
        }

        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
