namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.UI;

    public partial class ChartController
    {
        [SourceCodeFile("Model", "~/Models/SalesData.cs")]
        public ActionResult MissingValues(ChartLineMissingValues? missingValues)
        {
            ViewBag.MissingValues = missingValues ?? ChartLineMissingValues.Gap;
            return View(SalesDataBuilder.GetCollectionWithMissingValues());
        }
    }
}