using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using CaptchaSupport;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    public class FeedbackController : BaseController
    {
        //
        // GET: /Feedback/

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
                    ViewData.ModelState.AddModelError("CaptchaError", Resources.Feedback.Index.CaptchaError);
                    
                    model.CaptchaText = "";
                    TempData["FeedbackModel"] = model;
                    TempData["ViewData"] = ViewData;

                    return RedirectToAction("Index", "Home");
                }

                db.FeedBacks.Add(new FeedBack { Checked = false, EmailAddress = model.EmailAddress, Message = model.Message, SendDate = DateTime.Now });
                
                try
                {
                    db.SaveChanges();

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Feedback.Index.Success;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
                }


            }

            if ((string)Session["CaptchaString"] != model.CaptchaText)
            {
                ViewData.ModelState.AddModelError("CaptchaError", Resources.Feedback.Index.CaptchaError);
                model.CaptchaText = "";
            }

            model.CaptchaText = "";
            TempData["FeedbackModel"] = model;
            TempData["ViewData"] = ViewData;
            return RedirectToAction("Index", "Home");
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

            db.FeedBacks.Remove(element);
            try
            {
                db.SaveChanges();

                TempData["GlobalMessageType"] = MessageTypes.Success;
                TempData["ViewBag.GlobalMessage"] = Resources.Feedback.Index.Success;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;
            }
            catch (Exception)
            {
                TempData["GlobalMessageType"] = MessageTypes.Error;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
            }

            return RedirectToAction("Index", "Admin");
        }

        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }
    }
}
