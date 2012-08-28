using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using System.IO;
using System.Web.Security;
using Telerik.Web.Mvc.UI;
using Telerik.Web.Mvc.Extensions;
using System.Web.Routing;

namespace WebApplication.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /User/

        [LoginAuthorize]
        public ActionResult Index()
        {
            var model = db.Users.Where(e => e.Type != (int)UserTypes.SuperAdministrator).OrderByDescending(e => e.RegistrationDate).Select(e => new EditableUser { ActivationCode = e.ActivationCode, EmailAddress = e.EmailAddress, Id = e.Id, IsActivated = e.IsActivated, Password = "●●●●●●●", RegistrationDate = e.RegistrationDate, Type = (UserTypes)e.Type, Username = e.Username });
            PopulateUserTypes();
            return View(model);
        }

        private void PopulateUserTypes()
        {
            ViewData["UserTypes"] = new List<UserTypes> { UserTypes.Reader, UserTypes.Administrator, UserTypes.SuperAdministrator };
        }

        [HttpPost]
        public ActionResult ChangePassword(NewPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse((string)Session["UserId"]);
                var user = db.Users.Where(e => e.Id == id).First();
                if (user.Password != Common.CalculateMD5Hash(model.OldPassword))
                {
                    ViewData.ModelState.AddModelError("OldPassword", Resources.Common.WrongPassword);

                    TempData["NewPasswordModel"] = model;
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index", "Home");
                }

                user.Password = Common.CalculateMD5Hash(model.NewPassword);

                try
                {
                    db.SaveChanges();

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.PwChanged;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;
                }
                catch (Exception)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
                }

                return RedirectToAction("Index", "Home");
            }

            TempData["NewPasswordModel"] = model;
            TempData["ViewData"] = ViewData;

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangeEmail(NewEmailModel model)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse((string)Session["UserId"]);
                var user = db.Users.Where(e => e.Id == id).First();
                if (user.Password != Common.CalculateMD5Hash(model.EmailOldPassword))
                {
                    ViewData.ModelState.AddModelError("EmailOldPassword", Resources.Common.WrongPassword);

                    TempData["NewEmailModel"] = model;
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index", "Home");
                }

                user.EmailAddress = model.NewEmail;

                try
                {
                    db.SaveChanges();

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.EmailChanged;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;
                }
                catch (Exception)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
                }

                return RedirectToAction("Index", "Home");
            }

            TempData["NewEmailModel"] = model;
            TempData["ViewData"] = ViewData;

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult ForgottenPassword(ForgottenPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(e => e.EmailAddress == model.ForEmailAddress && e.Username == model.ForUserName).FirstOrDefault();
                if (user == null)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.UserNotFound;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
                }
                else
                {
                    string newPassword = Membership.GeneratePassword(7, 0);
                    user.Password = Common.CalculateMD5Hash(newPassword);
                    try
                    {
                        db.SaveChanges();

                        Common.SendNewPasswordMail(user.EmailAddress, newPassword, user.Username);

                        TempData["GlobalMessageType"] = MessageTypes.Success;
                        TempData["ViewBag.GlobalMessage"] = Resources.Common.NewPassword;
                        TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;
                    }
                    catch (Exception)
                    {
                        TempData["GlobalMessageType"] = MessageTypes.Error;
                        TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                        TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            TempData["ForgottenPasswordModel"] = model;
            TempData["ViewData"] = ViewData;

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult RegistrationActivation(string code)
        {
            var user = db.Users.Where(e => e.ActivationCode == code).FirstOrDefault();
            if (user == null)
            {
                TempData["GlobalMessageType"] = MessageTypes.Error;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.UserNotFound;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
            }
            else if (user.IsActivated)
            {
                TempData["GlobalMessageType"] = MessageTypes.Information;
                TempData["ViewBag.GlobalMessage"] = Resources.Common.UserAlreadyActivated;
                TempData["ViewBag.GlobalHeader"] = Resources.Common.Information;
            }
            else
            {
                user.IsActivated = true;
                try
                {
                    db.SaveChanges();

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.UserActivated;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;
                }
                catch (Exception)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool error = false;
                var c = (string)Session["CaptchaString"];
                if ((string)Session["CaptchaString"] != model.RegCaptchaText)
                {
                    ViewData.ModelState.AddModelError("RegCaptchaError", Resources.Feedback.Index.CaptchaError);
                    error = true;
                }

                if (model.RegUserName.ToLower().Contains("vialpando") || model.RegUserName.ToLower().Contains("vi4lp4ndo") || db.Users.Where(e => e.Username == model.RegUserName).FirstOrDefault() != null)
                {
                    ViewData.ModelState.AddModelError("RegUserName", Resources.Common.UsernameUsed);
                    error = true;
                }

                if (error)
                {
                    model.RegCaptchaText = "";
                    TempData["RegisterModel"] = model;
                    TempData["ViewData"] = ViewData;

                    return RedirectToAction("Index", "Home");
                }

                string code = Guid.NewGuid().ToString();
                db.Users.Add(new User { IsActivated = false, ActivationCode = code, Username = model.RegUserName, Password = Common.CalculateMD5Hash(model.RegPassword), EmailAddress = model.RegEmail, RegistrationDate = DateTime.Now, Type = (int)UserTypes.Reader });

                try
                {
                    db.SaveChanges();

                    Common.SendValidationMail(model.RegEmail, code, model.RegUserName);

                    TempData["GlobalMessageType"] = MessageTypes.Success;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ActivationSent + " (" + model.RegEmail + ")";
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Success;

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    TempData["GlobalMessageType"] = MessageTypes.Error;
                    TempData["ViewBag.GlobalMessage"] = Resources.Common.ErrorDatabase;
                    TempData["ViewBag.GlobalHeader"] = Resources.Common.Error;

                    return RedirectToAction("Index", "Home");
                }


            }

            if ((string)Session["RegCaptchaString"] != model.RegCaptchaText)
            {
                ViewData.ModelState.AddModelError("CaptchaError", Resources.Feedback.Index.CaptchaError);
                model.RegCaptchaText = "";
            }
            if (db.Users.Where(e => e.Username == model.RegUserName).FirstOrDefault() != null)
            {
                ViewData.ModelState.AddModelError("UserName", Resources.Common.UsernameUsed);
            }
            model.RegCaptchaText = "";
            TempData["RegisterModel"] = model;
            TempData["ViewData"] = ViewData;
            return RedirectToAction("Index", "Home");
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(int id)
        {
            //Create a new instance of the EditableProduct class and set its ProductID property.
            EditableUser user = new EditableUser
            {
                Id = id
            };
            //Perform model binding (fill the product properties and validate it).
            if (TryUpdateModel(user))
            {
                var u = db.Users.Where(e => e.Id == id).First();
                if (user.Password != "●●●●●●●")
                {
                    u.Password = Common.CalculateMD5Hash(user.Password);
                }
                u.EmailAddress = user.EmailAddress;
                u.IsActivated = user.IsActivated;
                u.RegistrationDate = user.RegistrationDate;
                u.Type = (int)user.Type;
                u.Username = user.Username;

                //The model is valid - update the product and redisplay the grid.
                db.Entry(u).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                RouteValueDictionary routeValues = this.GridRouteValues();
                // add the editing mode to the route values
                return RedirectToAction("Index", routeValues);
            }
            //The model is invalid - render the current view to show any validation errors
            return View("Index");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id)
        {
            //Find a product with ProductID equal to the id action parameter
            User user = db.Users.Where(p => p.Id == id).First();
            RouteValueDictionary routeValues;
            if (user == null)
            {
                //A product with the specified id does not exist - redisplay the grid
                //GridRouteValues() is an extension method which returns the 
                //route values defining the grid state - current page, sort expression, filter etc.
                routeValues = this.GridRouteValues();
                // add the editing mode to the route values
                // add button type to the route values
                return RedirectToAction("Index", routeValues);
            }

            //Delete the record
            db.Users.Remove(user);
            db.SaveChanges();
            routeValues = this.GridRouteValues();
            // add the editing mode to the route values
            // add button type to the route values
            //Redisplay the grid
            return RedirectToAction("Index", routeValues);
        }

    }
}
