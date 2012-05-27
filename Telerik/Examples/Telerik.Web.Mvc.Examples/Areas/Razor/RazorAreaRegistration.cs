namespace Telerik.Web.Mvc.Examples.Areas.Razor
{
#if MVC3
    using System.Web.Mvc;

    public class RazorAreaRegistration : AreaRegistration
    {
        public const string RazorViewToken = "IsRazorView";

        public override string AreaName
        {
            get
            {
                return "Razor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var razorRoute = context.MapRoute(
                "Razor_default",
                "razor/{controller}/{action}/{id}",
                new { controller = "Home", action = "FirstLook", id = "" });
            
            // The 'UseNamespaceFallback' token will allow the runtime to use the controllers defined outside the area
            razorRoute.DataTokens["UseNamespaceFallback"] = true;

            razorRoute.DataTokens[RazorViewToken] = true;           
        }
    }
#endif
}
