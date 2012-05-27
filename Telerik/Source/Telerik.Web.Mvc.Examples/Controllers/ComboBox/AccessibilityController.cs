namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System.Linq;
    using Telerik.Web.Mvc.Examples.Models;
    
    public partial class ComboBoxController : Controller
    {
        public ActionResult Accessibility()
        {
            return View(new NorthwindDataContext().Products.ToList());
        }
    }
}