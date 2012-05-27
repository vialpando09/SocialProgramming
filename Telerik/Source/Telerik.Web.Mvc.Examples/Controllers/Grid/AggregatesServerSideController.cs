namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class GridController : Controller
    {
        public ActionResult AggregatesServerSide()
        {
            var northwind = new NorthwindDataContext();

            return View(northwind.Products);
        }
    }
}