namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class NumericTextBoxController : Controller
    {
        [SourceCodeFile(
            FileName = "~/Views/Shared/EditorTemplates/Currency.ascx",
            RazorFileName = "~/Areas/Razor/Views/Shared/EditorTemplates/Currency.cshtml")]
        [SourceCodeFile(
            FileName = "~/Views/Shared/EditorTemplates/Integer.ascx",
            RazorFileName = "~/Areas/Razor/Views/Shared/EditorTemplates/Integer.cshtml")]
        [SourceCodeFile("EditableProduct.cs", "~/Models/EditableProduct.cs")]
        public ActionResult EditTemplate(EditableProduct product)
        {
            if (product.ProductID == 0)
            {
                product.ProductID = 1;
                product.ProductName = "Chai";
                product.UnitsInStock = 39;
                product.UnitPrice = 18.00m;
                product.LastSupply = DateTime.Today;
            }
            else 
            {
                ViewData["product"] = product;
            }

            return View(product);
        }
    }
}