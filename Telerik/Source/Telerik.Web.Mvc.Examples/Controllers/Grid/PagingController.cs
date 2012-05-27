namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.UI;

    public partial class GridController : Controller
    {
        public ActionResult Paging(bool? pageInput, bool? nextPrevious, bool? numeric, GridPagerPosition? position, int? currentPage, bool? pageSize)
        {
            ViewData["pageInput"] = pageInput ?? false;
            ViewData["nextPrevious"] = nextPrevious ?? true;
            ViewData["numeric"] = numeric ?? true;
            ViewData["pageSize"] = pageSize ?? false;
            ViewData["position"] = position ?? GridPagerPosition.Bottom;
            ViewData["currentPage"] = currentPage ?? 1;

            return View(GetOrders());
        }

        [GridAction]
        public ActionResult _Paging()
        {
            return View(new GridModel<Order>
            {
                Data = GetOrders()
            });
        }
    }
}