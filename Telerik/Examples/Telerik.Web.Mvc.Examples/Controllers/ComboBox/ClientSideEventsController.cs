namespace Telerik.Web.Mvc.Examples
{
    using System.Linq;
    using System.Web.Mvc;

    public partial class ComboBoxController : Controller
    {
        public ActionResult ClientSideEvents()
        {
            using (var nw = new Telerik.Web.Mvc.Examples.Models.NorthwindDataContext())
            {
                return View(nw.Products.ToList());
            }
        }
    }
}