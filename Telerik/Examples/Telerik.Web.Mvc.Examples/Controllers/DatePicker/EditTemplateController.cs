namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class DatePickerController : Controller
    {
        [SourceCodeFile("OrderInfo.cs", "~/Models/OrderInfo.cs", Order = 1)]
        [SourceCodeFile(
            FileName = "~/Views/Shared/EditorTemplates/Date.ascx",
            RazorFileName = "~/Areas/Razor/Views/Shared/EditorTemplates/Date.cshtml")]
        [SourceCodeFile(
            FileName = "~/Views/Shared/EditorTemplates/Time.ascx",
            RazorFileName = "~/Areas/Razor/Views/Shared/EditorTemplates/Time.cshtml")]
        [SourceCodeFile(
            FileName = "~/Views/Shared/EditorTemplates/DateTime.ascx",
            RazorFileName = "~/Areas/Razor/Views/Shared/EditorTemplates/DateTime.cshtml")]
        public ActionResult EditTemplate(OrderInfo orderInfo)
        {
            if (orderInfo.OrderInfoID == 0)
            {
                orderInfo.OrderInfoID = 1;
                orderInfo.Delay = new DateTime(2010, 1, 1, 10, 0, 0);
                orderInfo.DeliveryDate = new DateTime(2010, 1, 1);
                orderInfo.OrderDateTime = new DateTime(2010, 1, 1, 10, 0, 0);
            }
            else
            {
                ViewData["orderInfo"] = orderInfo;
            }

            return View(orderInfo);
        }
    }
}