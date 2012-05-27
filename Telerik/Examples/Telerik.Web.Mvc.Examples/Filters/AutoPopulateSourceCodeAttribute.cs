namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class AutoPopulateSourceCodeAttribute : FilterAttribute, IResultFilter
    {
        private const string ViewPath = "~/Views/";
        private const string RazorViewPath = "~/Areas/Razor/Views/";
        private const string ControllerPath = "~/Controllers/";
        private const string DescriptionPath = "~/Content/";
        private const string MasterPagePath = ViewPath + "Shared/Site.Master";
        private const string LayoutPagePath = RazorViewPath + "Shared/_Layout.cshtml";

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewResult viewResult = filterContext.Result as ViewResult;

            if (viewResult != null)
            {
                HttpServerUtilityBase server = filterContext.HttpContext.Server;

                string controllerName = filterContext.RouteData.GetRequiredString("controller");

                string viewName =
                    !string.IsNullOrEmpty(viewResult.ViewName)
                        ? viewResult.ViewName
                        : filterContext.RouteData.GetRequiredString("action");

                string baseViewPath = filterContext.IsRazorView() ? RazorViewPath : ViewPath;
                string viewExtension = filterContext.IsRazorView() ? ".cshtml" : ".aspx";
                string currentViewPath = baseViewPath + controllerName + Path.AltDirectorySeparatorChar + viewName + viewExtension;

                string exampleControllerPath = ControllerPath + controllerName + Path.AltDirectorySeparatorChar + viewName + "Controller.cs";

                string descriptionPath =
                    server.MapPath(DescriptionPath + Path.AltDirectorySeparatorChar + controllerName + 
                    Path.AltDirectorySeparatorChar + "Descriptions" + 
                    Path.AltDirectorySeparatorChar + viewName + ".html");

                var viewData = filterContext.Controller.ViewData;

                if (System.IO.File.Exists(descriptionPath))
                {
                    var descriptionText = System.IO.File.ReadAllText(descriptionPath);
#if MVC3
                    viewData["Description"] = new HtmlString(descriptionText);
#else
                    viewData["Description"] = descriptionText;
#endif
                }

                var codeFiles = new Dictionary<string, string>();
                codeFiles["View"] = currentViewPath;
                codeFiles["Controller"] = exampleControllerPath;
                RegisterLayoutPages(filterContext, codeFiles);

                viewData["codeFiles"] = codeFiles;
            }
        }

        private void RegisterLayoutPages(ResultExecutingContext filterContext, Dictionary<string, string> codeFiles)
        {
            if (filterContext.IsRazorView())
            {
                codeFiles["_Layout.cshtml"] = LayoutPagePath;
            }
            else
            {
                codeFiles["Site.Master"] = MasterPagePath;
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // Do Nothing
        }
    }
}