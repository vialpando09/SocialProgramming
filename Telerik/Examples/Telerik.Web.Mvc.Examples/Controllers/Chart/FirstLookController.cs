namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.UI;

    public partial class ChartController
    {
        [SourceCodeFile("Model", "~/Models/SalesData.cs")]
        public ActionResult FirstLook(bool? stack, string seriesType, bool? showTitle, bool? showLegend, ChartLegendPosition? legendPosition)
        {
            ViewBag.Stack = stack ?? false;
            ViewBag.SeriesType = seriesType ?? "bar";
            ViewBag.ShowTitle = showTitle ?? true;
            ViewBag.ShowLegend = showLegend ?? true;
            ViewBag.LegendPosition = legendPosition ?? ChartLegendPosition.Bottom;
            return View(SalesDataBuilder.GetCollection());
        }
    }
}