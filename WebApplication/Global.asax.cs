using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcGlobalisationSupport;

namespace WebApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            const string DefaultRouteUrl = "{controller}/{action}/{id}";
            const string EntryRouteUrl = "Entries/{id}/{title}";
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteValueDictionary defaultRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            RouteValueDictionary entryRouteValueDictionary = new RouteValueDictionary(new { controller = "Entry", action = "Detailed", id = UrlParameter.Optional });
             
            routes.Add("DefaultGlobalised", new GlobalisedRoute(DefaultRouteUrl, defaultRouteValueDictionary));
            routes.Add("EntryGlobalised", new GlobalisedRoute(EntryRouteUrl, entryRouteValueDictionary));
            
            routes.Add("Default", new Route(DefaultRouteUrl, defaultRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Entry", new Route(EntryRouteUrl, entryRouteValueDictionary, new MvcRouteHandler()));
            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}