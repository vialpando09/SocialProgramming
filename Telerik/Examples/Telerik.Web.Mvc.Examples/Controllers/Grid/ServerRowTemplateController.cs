namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.Extensions;

    public partial class GridController : Controller
    {
        public ActionResult ServerRowTemplate()
        {
            var customers = SessionCustomerRepository.All();
            return View(customers);
        }

        public ActionResult ServerRowTemplate_Update(string id)
        {
            var customer = SessionCustomerRepository.One(c => c.CustomerID == id);

            if (customer != null)
            {
                if (TryUpdateModel(customer))
                {
                    SessionCustomerRepository.Update(customer);
                    return RedirectToAction("ServerRowTemplate", this.GridRouteValues());
                }
            }

            return View("ServerRowTemplate", SessionCustomerRepository.All());
        }

        public ActionResult ServerRowTemplate_Delete(string id)
        {
            var customer = SessionCustomerRepository.One(c => c.CustomerID == id);

            if (customer != null)
            {
                SessionCustomerRepository.Delete(customer);
            }

            return RedirectToAction("ServerRowTemplate", this.GridRouteValues());
        }
    }
}