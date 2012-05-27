namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Models;
    using System.Linq;
using Telerik.Web.Mvc.UI;

    public partial class GridController : Controller
    {
        public ActionResult OperationMode(GridOperationMode? operationMode)
        {
            ViewData["OperationMode"] = operationMode ?? GridOperationMode.Client;
            return View();
        }

        [GridAction]
        public ActionResult _OperationMode()
        {
            return View(new GridModel<OrderDto>
            {
                Data = GetOrderDto()
            });
        }
    }
}