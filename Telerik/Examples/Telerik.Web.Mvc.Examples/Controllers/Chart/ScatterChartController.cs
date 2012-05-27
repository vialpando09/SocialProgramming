namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class ChartController
    {
        [SourceCodeFile("Model", "~/Models/PricePerformance.cs")]
        public ActionResult ScatterChart()
        {
            return View();
        }

        public ActionResult _PricePerformance() 
        {
            return Json(PricePerformanceBuilder.GetCollection()); 
        }
    }
}