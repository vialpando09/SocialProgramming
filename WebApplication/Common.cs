using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Security.Policy;
using WebApplication.Controllers;
using System.Linq.Expressions;
using System.Web.Routing;
using WebApplication.Models;
using System.Net;
using System.Web.Mail;
using System.ComponentModel;
using System.IO;
using System.Web;

namespace WebApplication
{
    public class EditableUser
    {

        [ReadOnly(true)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [UIHint("Type"), Required]
        public UserTypes Type { get; set; }
        [ReadOnly(true)]
        public string ActivationCode { get; set; }
        public bool IsActivated { get; set; }
        [DataType(DataType.Date), Required]
        public System.DateTime RegistrationDate { get; set; }
        public string EmailAddress { get; set; }
    }

    public enum UserTypes { Reader = 1, Administrator = 2, SuperAdministrator = 3 }

    public enum MessageTypes { Success, Information, Error, Warning }

    public static class LinkExtensions
    {
        public static MvcHtmlString ActionLinkButton(this HtmlHelper helper, UrlHelper url, string action, string controller, object routeValues, string value)
        {
            string result = "<input type=\"button\" onclick=\"javascript:window.location.href='" + url.Action(action, controller, routeValues) + "'\" value=\"" + value + "\" />";
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString ActionLinkButton(this HtmlHelper helper, string url, string value)
        {
            string result = "<input type=\"button\" onclick=\"javascript:window.location.href='" + url + "'\" value=\"" + value + "\" />";
            return new MvcHtmlString(result);
        }
    }

    public class LoginAuthorize : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserType"] != null && (UserTypes)filterContext.HttpContext.Session["UserType"] >= UserTypes.Administrator)
            {
                var result = filterContext.Result as ViewResult;
                if (result != null)
                    result.MasterName = "_Administration.cshtml";
                using (var db = new ModelContainer())
                {
                    int id = int.Parse((string)filterContext.HttpContext.Session["UserId"]);
                    filterContext.Controller.ViewBag.Username = db.Users.Single(e => e.Id == id).Username;
                }
            }
            else
            {
                filterContext.Controller.ViewBag.GlobalMessage = WebApplication.Resources.Common.NotPermisson;
                filterContext.Controller.ViewBag.GlobalHeader = WebApplication.Resources.Common.Error;
                filterContext.HttpContext.Response.Redirect("/");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string controller = (string)filterContext.RouteData.Values["controller"];
            if (controller == "Upload")
                return;

            if (filterContext.HttpContext.Session["UserType"] != null && (UserTypes)filterContext.HttpContext.Session["UserType"] >= UserTypes.Administrator)
            {
                var result = filterContext.Result as ViewResult;
                if (result != null)
                    result.MasterName = "_Administration";
            }
        }
    }

    public class NormalLoginAuthorize : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserType"] != null && (UserTypes)filterContext.HttpContext.Session["UserType"] >= UserTypes.Reader)
            {
                var result = filterContext.Result as ViewResult;
                using (var db = new ModelContainer())
                {
                    int id = int.Parse((string)filterContext.HttpContext.Session["UserId"]);
                    filterContext.Controller.ViewBag.Username = db.Users.Single(e => e.Id == id).Username;
                }
            }
            else
            {
                filterContext.Controller.ViewBag.GlobalMessage = WebApplication.Resources.Common.NotPermisson;
                filterContext.Controller.ViewBag.GlobalHeader = WebApplication.Resources.Common.Error;
                filterContext.HttpContext.Response.Redirect("/");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string controller = (string)filterContext.RouteData.Values["controller"];
            if (controller == "Upload")
                return;

            if (filterContext.HttpContext.Session["UserType"] != null && (UserTypes)filterContext.HttpContext.Session["UserType"] >= UserTypes.Reader)
            {
                var result = filterContext.Result as ViewResult;
            }
        }
    }

    public class GlobalAutoLoginAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ModelContainer db = ((BaseController)filterContext.Controller).Db;

            if (filterContext.RequestContext.HttpContext.Session["UserType"] == null)
            {
                string cookieName = "VialpandoBlogAuth";
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[cookieName];

                if (myCookie != null)
                {
                    int userId;
                    bool result = int.TryParse(myCookie.Values["UserId"], out userId);
                    if (!result)
                        return;
                    string userHash = myCookie.Values["UserData"];
                    var user = db.Users.Where(e => e.Id == userId && e.CookieHash == userHash).FirstOrDefault();

                    if (user != null)
                    {
                        filterContext.RequestContext.HttpContext.Session["UserType"] = (UserTypes)user.Type;
                        filterContext.RequestContext.HttpContext.Session["UserId"] = user.Id.ToString();
                        filterContext.RequestContext.HttpContext.Session["UserName"] = user.Username;

                        myCookie.Expires = DateTime.Now.AddDays(20);
                        HttpContext.Current.Response.Cookies.Add(myCookie);
                    }
                }
            }        
        }
    }

    public class BasicAction : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ModelContainer db = ((BaseController)filterContext.Controller).Db;
            dynamic viewBag = filterContext.Controller.ViewBag;
            dynamic tempData = filterContext.Controller.TempData;

            if (tempData["ViewData"] != null)
            {
                filterContext.Controller.ViewData = (ViewDataDictionary)tempData["ViewData"];
            }

            viewBag.Tags = db.Keywords;
            viewBag.Categories = db.Categories;
            viewBag.Archive = db.Entries.Where(e => e.Published).Select(e => e.PublishedDate);

            viewBag.AjaxType = "";
            viewBag.AjaxSecondParam = "";

            viewBag.GlobalMessageType = tempData["GlobalMessageType"];
            viewBag.GlobalHeader = tempData["ViewBag.GlobalHeader"];
            viewBag.GlobalMessage = tempData["ViewBag.GlobalMessage"];

            viewBag.Pages = db.Pages.Where(e => e.Published);

            if (tempData["FeedbackModel"] == null)
                viewBag.FeedbackModel = new FeedbackModel();
            else
            {
                viewBag.displayDialog = "FeedbackModel";
                viewBag.FeedbackModel = tempData["FeedbackModel"];
            }

            if (tempData["RegisterModel"] == null)
                viewBag.RegisterModel = new RegisterModel();
            else
            {
                viewBag.displayDialog = "RegisterModel";
                viewBag.RegisterModel = tempData["RegisterModel"];
            }

            if (tempData["ForgottenPasswordModel"] == null)
                viewBag.ForgottenPasswordModel = new ForgottenPasswordModel();
            else
            {
                viewBag.displayDialog = "ForgottenPasswordModel";
                viewBag.ForgottenPasswordModel = tempData["ForgottenPasswordModel"];
            }

            if (tempData["NewPasswordModel"] == null)
                viewBag.NewPasswordModel = new NewPasswordModel();
            else
            {
                viewBag.displayDialog = "NewPasswordModel";
                viewBag.NewPasswordModel = tempData["NewPasswordModel"];
            }

            if (tempData["NewEmailModel"] == null)
                viewBag.NewEmailModel = new NewEmailModel();
            else
            {
                viewBag.displayDialog = "NewEmailModel";
                viewBag.NewEmailModel = tempData["NewEmailModel"];
            }

            if (filterContext.HttpContext.Session["UserType"] != null)
            {
                viewBag.Logged = true;
                viewBag.Username = filterContext.HttpContext.Session["Username"].ToString();
                filterContext.Controller.ViewBag.Usertype = filterContext.HttpContext.Session["UserType"];
            }
            else
                viewBag.Logged = false;
            viewBag.LoginModel = new LoginModel();



        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EmailAddressAttribute : DataTypeAttribute, IClientValidatable
    {
        private static Regex _regex = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);


        public EmailAddressAttribute()
            : base(DataType.EmailAddress)
        {
            ErrorMessage = Resources.Common.EmailValidation;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ValidationType = "email",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
        }


        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }


            string valueAsString = value as string;
            return valueAsString != null && _regex.Match(valueAsString).Length > 0;
        }
    }

    public static class Common
    {
        public static string ToString(this UserTypes type)
        {
            if (UserTypes.Reader == type)
            {
                return Resources.Common.Reader;
            }
            if (UserTypes.Administrator == type)
            {
                return Resources.Common.Administrator;
            }
            if (UserTypes.SuperAdministrator == type)
            {
                return Resources.Common.SuperAdministrator;
            }

            return "";
        }

        public static string SiteName = "VialpandoBlog";
        public static string SiteAddress = "http://localhost:39304/";
        public static string NoReplyPassword = "DreBr5wE";
        public static string NoReplyAddress = "vialpando.blog@gmail.com";
        public static string SmtpServer = "smtp.gmail.com";
        public static int SmtpPort = 587;

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes, string labelText)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes), labelText);
        }
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string labelText)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static string Urlable(this string text)
        {
            text = text.ToLower();
            text = text.Replace('ö', 'o');
            text = text.Replace('ő', 'o');
            text = text.Replace('ó', 'o');
            text = text.Replace('ü', 'u');
            text = text.Replace('ú', 'u');
            text = text.Replace('ű', 'u');
            text = text.Replace('í', 'i');
            text = text.Replace('á', 'a');
            text = text.Replace('é', 'e');
            text = text.Replace(' ', '_');
            text = text.Replace('#', '_');
            text = text.Replace('$', '_');
            text = text.Replace('.', '_');
            text = text.Replace('&', '_');

            return text;
        }

        public static string CalculateMD5Hash(string input)
        {
            if (input == null)
                input = "";
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static void SendValidationMail(string toAddress, string code, string username)
        {
            // Passing the values and make a email formate to display
            string subject = SiteName + " - Aktiváció/Activation";
            string link = SiteAddress + "User/RegistrationActivation/?code=" + code;
            string body = "<p>Hi " + username + "!</p>";
            body += "<p>Thanks for your registration! Here is your activation link: ";
            body += "<a href=\"" + link + "\">" + link + "</a></p>";
            body += "<p>Click on the link, or copy it into your browser's address field.</p>";
            body += "______________________________<br />";
            body += "<p>Szia " + username + "!</p>";
            body += "<p>Köszi, hogy regisztráltál! Itt az aktivációs linked: ";
            body += "<a href=\"" + link + "\">" + link + "</a></p>";
            body += "<p>Kattints rá, vagy másold a böngésződ cím mezőjébe.</p>";

            var message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress(NoReplyAddress);
            message.To.Add(new System.Net.Mail.MailAddress(toAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = SmtpServer;
                smtp.Port = SmtpPort;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(NoReplyAddress, NoReplyPassword);
                smtp.SendAsync(message, null);
            }
        }

        public static void SendNewPasswordMail(string toAddress, string password, string username)
        {
            // Passing the values and make a email formate to display
            string subject = SiteName + " - Elfelejtett jelszó/Forgotten password";
            string body = "<p>Hi " + username + "!</p>";
            body += "<p>Here is your new password: " + password;
            body += "<p>Now you can log in again.</p>";
            body += "______________________________<br />";
            body += "<p>Szia " + username + "!</p>";
            body += "<p>Itt az új jelszavad: " + password;
            body += "<p>Most már megint be tudsz lépni.</p>";

            var message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress(NoReplyAddress);
            message.To.Add(new System.Net.Mail.MailAddress(toAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = SmtpServer;
                smtp.Port = SmtpPort;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(NoReplyAddress, NoReplyPassword);
                smtp.SendAsync(message, null);
            }
        }

        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext); // henter info fra windows registry
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            else if (ext == ".png") // a couple of extra info, due to missing information on the server
            {
                mimeType = "image/png";
            }
            else if (ext == ".flv")
            {
                mimeType = "video/x-flv";
            }
            else if (ext == ".avi")
            {
                mimeType = "application/x-troff-msvideo";
            }
            else if (ext == ".bin")
            {
                mimeType = "application/x-binary";
            }
            else if (ext == ".bmp")
            {
                mimeType = "image/bmp";
            }
            else if (ext == ".book")
            {
                mimeType = "application/book";
            }
            else if (ext == ".boz")
            {
                mimeType = "application/x-bzip2";
            }
            else if (ext == ".bsh")
            {
                mimeType = "application/x-bsh";
            }
            else if (ext == ".bz")
            {
                mimeType = "application/x-bzip";
            }
            else if (ext == ".bz2")
            {
                mimeType = "application/x-bzip2";
            }
            else if (ext == ".c")
            {
                mimeType = "text/plain";
            }
            else if (ext == ".c++")
            {
                mimeType = "text/plain";
            }
            else if (ext == ".cc")
            {
                mimeType = "text/plain";
            }
            else if (ext == ".class")
            {
                mimeType = "application/java";
            }
            else if (ext == ".cpp")
            {
                mimeType = "text/x-c";
            }
            else if (ext == ".css")
            {
                mimeType = "text/css";
            }
            else if (ext == ".cxx")
            {
                mimeType = "text/plain";
            }
            else if (ext == ".doc")
            {
                mimeType = "application/msword";
            }
            else if (ext == ".docx")
            {
                mimeType = "application/msword";
            }
            else if (ext == ".dump")
            {
                mimeType = "application/octet-stream";
            }
            else if (ext == ".dvi")
            {
                mimeType = "application/x-dvi";
            }
            else if (ext == ".eps")
            {
                mimeType = "application/postscript";
            }
            else if (ext == ".exe")
            {
                mimeType = "application/octet-stream";
            }
            else if (ext == ".f")
            {
                mimeType = "text/x-fortran";
            }
            else if (ext == ".gif")
            {
                mimeType = "image/gif";
            }
            else if (ext == ".gz")
            {
                mimeType = "application/x-compressed";
            }
            else if (ext == ".gzip")
            {
                mimeType = "application/x-gzip";
            }
            else if (ext == ".h")
            {
                mimeType = "text/x-h";
            }
            else if (ext == ".help")
            {
                mimeType = "application/x-helpfile";
            }
            else if (ext == ".htm")
            {
                mimeType = "text/html";
            }
            else if (ext == ".html")
            {
                mimeType = "text/html";
            }
            else if (ext == ".htmls")
            {
                mimeType = "text/html";
            }
            else if (ext == ".htt")
            {
                mimeType = "text/webviewhtml";
            }
            else if (ext == ".htx")
            {
                mimeType = "text/html";
            }
            else if (ext == ".ico")
            {
                mimeType = "image/x-icon";
            }
            else if (ext == ".imap")
            {
                mimeType = "application/x-httpd-imap";
            }
            else if (ext == ".java")
            {
                mimeType = "text/x-java-source";
            }
            else if (ext == ".jpe")
            {
                mimeType = "image/jpeg";
            }
            else if (ext == ".jpeg")
            {
                mimeType = "image/jpeg";
            }
            else if (ext == ".jpg")
            {
                mimeType = "image/jpeg";
            }
            else if (ext == ".js")
            {
                mimeType = "application/x-javascript";
            }
            else if (ext == ".latex")
            {
                mimeType = "application/x-latex";
            }
            else if (ext == ".log")
            {
                mimeType = "text/plain";
            }
            else if (ext == ".mid")
            {
                mimeType = "application/x-midi";
            }
            else if (ext == ".midi")
            {
                mimeType = "application/x-midi";
            }
            else if (ext == ".mime")
            {
                mimeType = "message/rfc822";
            }
            else if (ext == ".mov")
            {
                mimeType = "video/quicktime";
            }
            else if (ext == ".movie")
            {
                mimeType = "video/x-sgi-movie";
            }
            else if (ext == ".mp2")
            {
                mimeType = "audio/mpeg";
            }
            else if (ext == ".mp3")
            {
                mimeType = "audio/mpeg3";
            }
            else if (ext == ".mpe")
            {
                mimeType = "video/mpeg";
            }
            else if (ext == ".mpeg")
            {
                mimeType = "video/mpeg";
            }
            else if (ext == ".mpg")
            {
                mimeType = "audio/mpeg";
            }
            else if (ext == ".ms")
            {
                mimeType = "application/x-troff-ms";
            }
            else if (ext == ".pdf")
            {
                mimeType = "application/pdf";
            }
            else if (ext == ".pic")
            {
                mimeType = "image/pict";
            }
            else if (ext == ".pict")
            {
                mimeType = "image/pict";
            }
            else if (ext == ".pps")
            {
                mimeType = "application/mspowerpoint";
            }
            else if (ext == ".ppt")
            {
                mimeType = "application/mspowerpoint";
            }
            else if (ext == ".psd")
            {
                mimeType = "application/octet-stream";
            }
            else if (ext == ".py")
            {
                mimeType = "text/x-script.phyton";
            }
            else if (ext == ".rpm")
            {
                mimeType = "audio/x-pn-realaudio-plugin";
            }
            else if (ext == ".rt")
            {
                mimeType = "text/richtext";
            }
            else if (ext == ".rtf")
            {
                mimeType = "application/rtf";
            }
            else if (ext == ".rtx")
            {
                mimeType = "application/rtf";
            }
            else if (ext == ".sh")
            {
                mimeType = "application/x-bsh";
            }
            else if (ext == ".shtml")
            {
                mimeType = "text/html";
            }
            else if (ext == ".svf")
            {
                mimeType = "image/vnd.dwg";
            }
            else if (ext == ".swf")
            {
                mimeType = "application/x-shockwave-flash";
            }
            else if (ext == ".txt")
            {
                mimeType = "text/plain";
            }
            else if (ext == ".word")
            {
                mimeType = "application/msword";
            }
            else if (ext == ".xl")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xla")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xlb")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xlc")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xld")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xlk")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xll")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xlm")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xls")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xlt")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xlv")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xlw")
            {
                mimeType = "application/excel";
            }
            else if (ext == ".xml")
            {
                mimeType = "text/xml";
            }
            else if (ext == ".zip")
            {
                mimeType = "application/x-zip-compressed";
            }
            return mimeType;
        }
    }
}