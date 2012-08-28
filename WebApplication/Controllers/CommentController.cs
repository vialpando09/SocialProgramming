using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace WebApplication.Controllers
{
    public class CommentController : BaseController
    {
        //
        // GET: /Comment/

        [NormalLoginAuthorize]
        [HttpPost]
        public ActionResult Add(string message, int entryId)
        {

            var entry = db.Entries.SingleOrDefault(e => e.Id == entryId);
            var userId = int.Parse((string)Session["UserId"]);
            var user = db.Users.SingleOrDefault(e => e.Id == userId);
            if (entry != null && user != null )
            {
                try
                {
                    var comment = new Comment { Content = message, Date = DateTime.Now, Entry = entry, User = user };

                    db.Comments.Add(comment);

                    db.SaveChanges();

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.CommentSend;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;

                    return Redirect(Request.UrlReferrer.ToString());
                }
                catch (DbEntityValidationException e)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;

                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            TempData["GlobalMessageType"] = MessageTypes.Error;
            TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
            TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;

            return Redirect(Request.UrlReferrer.ToString());
        }

        [NormalLoginAuthorize]
        public ActionResult Delete(int id)
        {

            var comment = db.Comments.SingleOrDefault(e => e.Id == id);
            if (comment != null && ((UserTypes)Session["UserType"] >= UserTypes.Administrator || comment.User.Id == int.Parse((string)Session["UserId"])))
            {
                try
                {
                    comment.User.Comments.Remove(comment);
                    comment.Entry.Comments.Remove(comment);

                    db.Comments.Remove(comment);
                    
                    db.SaveChanges();

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.CommentSend;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;

                    return Redirect(Request.UrlReferrer.ToString());
                }
                catch (Exception)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;

                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            TempData["GlobalMessageType"] = MessageTypes.Error;
            TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
            TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
