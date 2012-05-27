namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class GridController : Controller
    {
        public ActionResult AggregatesAjax()
        {
            return View();
        }

        [GridAction]
        public ActionResult AggregatesAjax_Select()
        {
            var northwind = new NorthwindDataContext();

            return View(new GridModel(northwind.Products));
        }
    }
}