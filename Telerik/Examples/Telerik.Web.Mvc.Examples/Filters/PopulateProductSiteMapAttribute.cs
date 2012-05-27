namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;

    public class PopulateProductSiteMapAttribute : PopulateSiteMapAttribute
    {
        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);

            var fullSiteMap = (XmlSiteMap)filterContext.Controller.ViewData[ViewDataKey];
            var productSiteMap = new XmlSiteMap();
            productSiteMap.RootNode = new SiteMapNode();

            foreach (SiteMapNode node in fullSiteMap.RootNode.ChildNodes)
            {
                var controller = node.ControllerName ?? node.Title;
                var action = node.ActionName ?? "firstlook";
                var clone = new SiteMapNode
                {
                    ActionName = action,
                    ControllerName = controller,
                    Title = node.Title
                };

                clone.Attributes.Merge(node.Attributes);
                productSiteMap.RootNode.ChildNodes.Add(clone);
            }

            filterContext.Controller.ViewData["telerik.web.mvc.products"] = productSiteMap;

            var examplesSiteMap = new XmlSiteMap();
            
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var productSiteMapNode = fullSiteMap.RootNode.ChildNodes
                .FirstOrDefault(node => controllerName.Equals(node.Title, StringComparison.OrdinalIgnoreCase));

            if (productSiteMapNode != null && !controllerName.Equals("Home", StringComparison.OrdinalIgnoreCase))
            {
                examplesSiteMap.RootNode = new SiteMapNode();
                examplesSiteMap.RootNode.ChildNodes.Add(productSiteMapNode);
                filterContext.Controller.ViewData["telerik.web.mvc.products.examples"] = examplesSiteMap;
            }
        }
    }
}
