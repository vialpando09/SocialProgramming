namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using Telerik.Web.Mvc.Examples.Models;
    using Telerik.Web.Mvc.UI;

    [AutoPopulateSourceCode]
    [PopulateProductSiteMap(SiteMapName = "examples", ViewDataKey = "telerik.mvc.examples")]
    public partial class TreeViewController : Controller
    {
        private static IEnumerable<Category> GetCategories()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            return northwind.Categories;
        }

        private IEnumerable<TreeViewItem> GetCategoryTreeViewItem()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            return from item in northwind.Categories
                                select new TreeViewItem
                                {
                                    Text = item.CategoryName,
                                    Value = item.CategoryID.ToString()
                                };
        }

        private static IEnumerable<Employee> GetRootEmployees()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            
            return northwind.Employees.Where(e => e.ReportsTo == null);
        }
    }
}