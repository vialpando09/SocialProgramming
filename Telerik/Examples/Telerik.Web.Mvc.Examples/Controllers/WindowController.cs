namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;

    [AutoPopulateSourceCode]
    [PopulateProductSiteMap(SiteMapName = "examples", ViewDataKey = "telerik.mvc.examples")]
    public partial class WindowController : Controller
    {
        public ActionResult AjaxView()
        {
            return PartialView();
        }
    }
}