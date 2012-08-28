using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;

namespace WebApplication.Controllers
{
    public static class StaticUrlHelper
    {
        public static string Combine(this string domain, string parameter)
        {
            return domain.EndsWith("/") ? domain + parameter : domain + "/" + parameter; 
        }
    }

    public class SitemapController : BaseController
    {
        //
        // GET: /Sitemap/

        public ActionResult Generate()
        {
            string path = Path.Combine(Server.MapPath("~"), "sitemap.xml");

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);// Create the root element
            XmlElement root = doc.CreateElement("urlset", "http://www.google.com/schemas/sitemap/0.90");
            doc.AppendChild(root);

            string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host +
                            (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);

            //First
            {   
                {
                    XmlElement url = doc.CreateElement("url");
                    XmlElement loc = doc.CreateElement("loc");
                    XmlElement lastmod = doc.CreateElement("lastmod");
                    XmlElement changefreq = doc.CreateElement("changefreq");
                    XmlElement priority = doc.CreateElement("priority");
                    
                    root.AppendChild(url);

                    loc.InnerText = StaticUrlHelper.Combine(domain, "hu/");
                    lastmod.InnerText = DateTime.Now.ToShortDateString().Replace('.', '-') + "T" + DateTime.Now.ToLongTimeString() + "+" + TimeZoneInfo.Local.BaseUtcOffset.ToString();
                    changefreq.InnerText = "daily";
                    priority.InnerText = "0.5";

                    url.AppendChild(loc);
                    url.AppendChild(lastmod);
                    url.AppendChild(changefreq);
                    url.AppendChild(priority);
                }
                {
                    XmlElement url = doc.CreateElement("url");
                    XmlElement loc = doc.CreateElement("loc");
                    XmlElement lastmod = doc.CreateElement("lastmod");
                    XmlElement changefreq = doc.CreateElement("changefreq");
                    XmlElement priority = doc.CreateElement("priority");
                    
                    root.AppendChild(url);

                    loc.InnerText = StaticUrlHelper.Combine(domain, "en/");
                    lastmod.InnerText = DateTime.Now.ToShortDateString().Replace('.', '-') + "T" + DateTime.Now.ToShortTimeString() + "+" + TimeZoneInfo.Local.BaseUtcOffset.ToString(); ;
                    changefreq.InnerText = "daily";
                    priority.InnerText = "0.5";

                    url.AppendChild(loc);
                    url.AppendChild(lastmod);
                    url.AppendChild(changefreq);
                    url.AppendChild(priority);
                }
            }

            //Entries
            foreach (var item in db.Entries.Where(e => e.Published))
            {
                int year = item.PublishedDate.Year;
                int month = item.PublishedDate.Month;
                int day = item.PublishedDate.Day;
                string date = year.ToString() + ((month < 10) ? "0" : "") + month.ToString() + ((day < 10) ? "0" : "") + day.ToString();
                string itemUrl = "Entries/" + date + "/" + item.Id + "/";
                
                {
                    XmlElement url = doc.CreateElement("url");
                    XmlElement loc = doc.CreateElement("loc");
                    XmlElement lastmod = doc.CreateElement("lastmod");
                    XmlElement changefreq = doc.CreateElement("changefreq");
                    XmlElement priority = doc.CreateElement("priority");

                    root.AppendChild(url);

                    loc.InnerText = StaticUrlHelper.Combine(domain, "hu/") + itemUrl + item.huTitle.Urlable();
                    lastmod.InnerText = DateTime.Now.ToShortDateString().Replace('.', '-') + "T" + DateTime.Now.ToLongTimeString() + "+" + TimeZoneInfo.Local.BaseUtcOffset.ToString();
                    changefreq.InnerText = "daily";
                    priority.InnerText = "0.5";

                    url.AppendChild(loc);
                    url.AppendChild(lastmod);
                    url.AppendChild(changefreq);
                    url.AppendChild(priority);
                }
                {
                    XmlElement url = doc.CreateElement("url");
                    XmlElement loc = doc.CreateElement("loc");
                    XmlElement lastmod = doc.CreateElement("lastmod");
                    XmlElement changefreq = doc.CreateElement("changefreq");
                    XmlElement priority = doc.CreateElement("priority");

                    root.AppendChild(url);

                    loc.InnerText = StaticUrlHelper.Combine(domain, "en/") + itemUrl + item.huTitle.Urlable();
                    lastmod.InnerText = DateTime.Now.ToShortDateString().Replace('.', '-') + "T" + DateTime.Now.ToShortTimeString() + "+" + TimeZoneInfo.Local.BaseUtcOffset.ToString(); ;
                    changefreq.InnerText = "daily";
                    priority.InnerText = "0.5";

                    url.AppendChild(loc);
                    url.AppendChild(lastmod);
                    url.AppendChild(changefreq);
                    url.AppendChild(priority);
                }
            }

            //Pages
            foreach (var item in db.Pages.Where(e => e.Published))
            {
                int year = item.PublishedDate.Year;
                int month = item.PublishedDate.Month;
                int day = item.PublishedDate.Day;
                string date = year.ToString() + ((month < 10) ? "0" : "") + month.ToString() + ((day < 10) ? "0" : "") + day.ToString();
                string itemUrl = "Pages/" + item.Id + "/";

                {
                    XmlElement url = doc.CreateElement("url");
                    XmlElement loc = doc.CreateElement("loc");
                    XmlElement lastmod = doc.CreateElement("lastmod");
                    XmlElement changefreq = doc.CreateElement("changefreq");
                    XmlElement priority = doc.CreateElement("priority");

                    root.AppendChild(url);

                    loc.InnerText = StaticUrlHelper.Combine(domain, "hu/") + itemUrl + item.huTitle.Urlable();
                    lastmod.InnerText = DateTime.Now.ToShortDateString().Replace('.', '-') + "T" + DateTime.Now.ToLongTimeString() + "+" + TimeZoneInfo.Local.BaseUtcOffset.ToString();
                    changefreq.InnerText = "daily";
                    priority.InnerText = "0.5";

                    url.AppendChild(loc);
                    url.AppendChild(lastmod);
                    url.AppendChild(changefreq);
                    url.AppendChild(priority);
                }
                {
                    XmlElement url = doc.CreateElement("url");
                    XmlElement loc = doc.CreateElement("loc");
                    XmlElement lastmod = doc.CreateElement("lastmod");
                    XmlElement changefreq = doc.CreateElement("changefreq");
                    XmlElement priority = doc.CreateElement("priority");

                    root.AppendChild(url);

                    loc.InnerText = StaticUrlHelper.Combine(domain, "en/") + itemUrl + item.huTitle.Urlable();
                    lastmod.InnerText = DateTime.Now.ToShortDateString().Replace('.', '-') + "T" + DateTime.Now.ToShortTimeString() + "+" + TimeZoneInfo.Local.BaseUtcOffset.ToString(); ;
                    changefreq.InnerText = "daily";
                    priority.InnerText = "0.5";

                    url.AppendChild(loc);
                    url.AppendChild(lastmod);
                    url.AppendChild(changefreq);
                    url.AppendChild(priority);
                }
            }

            doc.Save(path);

            return File(path, "applcation/xml");
        }
    }
}
