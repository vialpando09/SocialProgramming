namespace Telerik.Web.Mvc.Examples
{
    using System.Linq;
    using System.Web.Mvc;
    using Models;

    using Extensions;
    using System.Collections.Generic;

    public partial class ComboBoxController : Controller
    {
        public ActionResult CascadingComboBox()
        {
            NorthwindDataContext nw = new NorthwindDataContext();

            return View(nw.Categories.ToList());
        }

        [HttpPost]
        public JsonResult _GetDropDownListProducts(int? DropDownList_Categories)
        {
            return _GetProducts(DropDownList_Categories);
        }

        [HttpPost]
        public JsonResult _GetComboboxProducts(int? ComboBox_Categories)
        {
            return _GetProducts(ComboBox_Categories);
        }

        [HttpPost]
        public JsonResult _GetDropDownListOrders(int? DropDownList_Products)
        {
            return _GetOrders(DropDownList_Products);
        }

        [HttpPost]
        public JsonResult _GetComboBoxOrders(int? Combobox_Products)
        {
            return _GetOrders(Combobox_Products);
        }

        private JsonResult _GetProducts(int? CategoryID)
        {
            NorthwindDataContext nw = new NorthwindDataContext();

            IQueryable<Product> products = nw.Products.AsQueryable<Product>();

            if (CategoryID.HasValue)
            {
                products = products.Where(p => p.CategoryID == CategoryID.Value);
            }

            return Json(new SelectList(products, "ProductID", "ProductName"), JsonRequestBehavior.AllowGet);
        }

        private JsonResult _GetOrders(int? ProductID)
        {
            NorthwindDataContext nw = new NorthwindDataContext();

            IList<Order> orders = new List<Order>();

            if (ProductID.HasValue)
            {
                orders = nw.Order_Details.Where(od => od.ProductID == ProductID).Select(od => od.Order).ToList();
            }

            return Json(new SelectList(orders, "OrderID", "ShipName"), JsonRequestBehavior.AllowGet);
        }
    }
}