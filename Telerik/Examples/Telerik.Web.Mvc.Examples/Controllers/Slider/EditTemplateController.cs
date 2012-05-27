namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;

    public partial class SliderController : Controller
    {
        [SourceCodeFile("ProductDto.cs", "~/Models/ProductDto.cs")]
        [SourceCodeFile(
            FileName = "~/Views/Slider/EditorTemplates/Slider.ascx",
            RazorFileName = "~/Areas/Razor/Views/Slider/EditorTemplates/Slider.cshtml")]
        public ActionResult EditTemplate(ProductDto product)
        {
            if (product.ProductID == 0)
            {
                product.ProductID = 1;
                product.ProductName = "Chai";
                product.UnitPrice = 18.00m;
            }
            else
            {
                ViewData["product"] = product;
            }
            return View(product);
        }
    }
}