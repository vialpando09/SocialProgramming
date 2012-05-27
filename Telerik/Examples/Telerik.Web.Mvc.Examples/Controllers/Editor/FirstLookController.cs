namespace Telerik.Web.Mvc.Examples
{
    using System.Web;
    using System.Web.Mvc;

    public partial class EditorController : Controller
    {
        [ValidateInput(false)]
        public ActionResult FirstLook(string editor)
        {
            if (editor != null)
            {
                ViewData["value"] = HttpUtility.HtmlEncode(editor.IndentHtml());
            }

            return View();
        }
    }
}