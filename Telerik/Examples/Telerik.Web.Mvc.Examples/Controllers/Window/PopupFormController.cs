namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class WindowController : Controller
    {
        [SourceCodeFile(Caption = "FeedbackMessage", FileName = "~/Models/FeedbackMessage.cs")]
        public ActionResult PopupForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PopupForm(FeedbackMessage message)
        {
            message.Comment = Server.HtmlDecode(message.Comment);
            ViewData["message"] = message;

            return View();
        }
    }
}