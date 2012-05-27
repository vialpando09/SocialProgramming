namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class ChartController
    {
        [SourceCodeFile("Model", "~/Models/SalesData.cs")]
        public ActionResult ClientSideApi()
        {
            return View();
        }

        public ActionResult _SalesDataByYear(int? year)
        {
            ViewBag.Year = year ?? 2010;

            return Json(
                SalesDataBuilder.GetCollection().FindAll(
                    s => s.DateString.Contains(ViewBag.Year.ToString())
                )
            );
        }
    }
}