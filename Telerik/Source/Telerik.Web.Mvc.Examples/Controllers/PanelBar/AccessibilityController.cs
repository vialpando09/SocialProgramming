namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using System.Linq;

    public partial class PanelBarController : Controller
    {
        public ActionResult Accessibility(string itemName)
        {
            ViewData["itemName"] = itemName;

            var list = new NorthwindDataContext().Categories.Take(2);

            return View(list);
        }
    }
}