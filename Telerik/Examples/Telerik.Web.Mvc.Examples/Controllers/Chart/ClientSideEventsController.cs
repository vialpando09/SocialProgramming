namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    public partial class ChartController
    {
        [SourceCodeFile("Model", "~/Models/SalesData.cs")]
        public ActionResult ClientSideEvents()
        {
            return View();
        }
    }
}