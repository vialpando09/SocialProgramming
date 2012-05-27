using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace WebApplication
{
    public static class LinkExtensions
    {
        public static MvcHtmlString ActionLinkButton(this HtmlHelper helper, UrlHelper url, string action, string controller, object routeValues, string value )
        {
            string result = "<input type=\"button\" onclick=\"javascript:window.location.href='" + url.Action(action, controller, routeValues) + "'\" value=\""+ value +"\" />";
            return new MvcHtmlString(result);   
        }
    }

    public class LoginAuthorize : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty((string)filterContext.HttpContext.Session["UserId"]))
            {
                filterContext.Controller.ViewBag.GlobalMessage = WebApplication.Resources.Common.NotPermisson;
                filterContext.Controller.ViewBag.GlobalHeader = WebApplication.Resources.Common.Error;
                filterContext.HttpContext.Response.Redirect("/");
            }
            else
            {
                var result = filterContext.Result as ViewResult;
                if (result != null)
                    result.MasterName = "_Administration.cshtml";
                using(var db = new ModelContainer())
                {
                    int id = int.Parse((string)filterContext.HttpContext.Session["UserId"]);
                    filterContext.Controller.ViewBag.Username = db.Users.Single(e => e.Id == id).Username;
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string controller = (string)filterContext.RouteData.Values["controller"];
            if (controller == "Upload")
                return;

            if (!string.IsNullOrEmpty((string)filterContext.HttpContext.Session["UserId"]))            
            {
                var result = filterContext.Result as ViewResult;
                if (result != null)
                    result.MasterName = "_Administration";
            }
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
    }
}