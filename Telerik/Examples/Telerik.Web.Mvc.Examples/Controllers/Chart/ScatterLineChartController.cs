namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class ChartController
    {
        [SourceCodeFile("Model", "~/Models/EngineData.cs")]
        public ActionResult ScatterLineChart()
        {
            return View(EngineData.Points);
        }
    }
}