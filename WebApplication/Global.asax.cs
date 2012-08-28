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
            const string SitemapRouteUrl = "sitemap.xml";
            const string PageRouteUrl = "Pages/{id}/{title}";
            const string EntryRouteUrl = "Entries/{date}/{id}/{title}";
            const string CategoryRouteUrl = "CategoryEntries/{id}/{title}";
            const string TagsRouteUrl = "Tag/{tag}";
            const string SearchRouteUrl = "Search/{filter}/";
            const string ArchiveRouteUrl = "Archive/{year}/{month}";
            const string ArchiveMoreRouteUrl = "Archive/More";
            const string LoginRouteUrl = "Login/";
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteValueDictionary defaultRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            RouteValueDictionary sitemapRouteValueDictionary = new RouteValueDictionary(new { controller = "Sitemap", action = "Generate" });
            RouteValueDictionary pageRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Page" });
            RouteValueDictionary entryRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Entry" });
            RouteValueDictionary categoryRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Category" });
            RouteValueDictionary tagsRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Tag" });
            RouteValueDictionary searchRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Search" });
            RouteValueDictionary archiveRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "Archive" });
            RouteValueDictionary archiveMoreRouteValueDictionary = new RouteValueDictionary(new { controller = "Home", action = "ArchiveMore" });
            RouteValueDictionary loginRouteValueDictionary = new RouteValueDictionary(new { controller = "Admin", action = "Login" });

            routes.Add("LoginGlobalised", new GlobalisedRoute(LoginRouteUrl, loginRouteValueDictionary));
            routes.Add("SitemapGlobalised", new GlobalisedRoute(SitemapRouteUrl, sitemapRouteValueDictionary));
            routes.Add("PageGlobalised", new GlobalisedRoute(PageRouteUrl, pageRouteValueDictionary));
            routes.Add("EntryGlobalised", new GlobalisedRoute(EntryRouteUrl, entryRouteValueDictionary));
            routes.Add("CategoryGlobalised", new GlobalisedRoute(CategoryRouteUrl, categoryRouteValueDictionary));
            routes.Add("TagsGlobalised", new GlobalisedRoute(TagsRouteUrl, tagsRouteValueDictionary));
            routes.Add("SearchGlobalised", new GlobalisedRoute(SearchRouteUrl, searchRouteValueDictionary));
            routes.Add("ArchiveGlobalised", new GlobalisedRoute(ArchiveRouteUrl, archiveRouteValueDictionary));
            routes.Add("ArchiveMoreGlobalised", new GlobalisedRoute(ArchiveMoreRouteUrl, archiveMoreRouteValueDictionary));
            routes.Add("DefaultGlobalised", new GlobalisedRoute(DefaultRouteUrl, defaultRouteValueDictionary));

            routes.Add("Login", new Route(LoginRouteUrl, loginRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Sitemap", new Route(SitemapRouteUrl, sitemapRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Page", new Route(PageRouteUrl, pageRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Entry", new Route(EntryRouteUrl, entryRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Category", new Route(CategoryRouteUrl, categoryRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Tags", new Route(TagsRouteUrl, tagsRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Search", new Route(SearchRouteUrl, searchRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Archive", new Route(ArchiveRouteUrl, archiveRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("ArchiveMore", new Route(ArchiveMoreRouteUrl, archiveMoreRouteValueDictionary, new MvcRouteHandler()));
            routes.Add("Default", new Route(DefaultRouteUrl, defaultRouteValueDictionary, new MvcRouteHandler()));
            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new GlobalAutoLoginAction());
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}