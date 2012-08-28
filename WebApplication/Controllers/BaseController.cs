using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        protected readonly ModelContainer db = new ModelContainer();
        public ModelContainer Db { get { return db; } }

        public ActionResult UploadImage(IEnumerable<HttpPostedFileBase> featuredImage)
        {
            // The Name of the Upload component is "attachments" 
            List<string> list = new List<string>();
            foreach (var file in featuredImage)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var fileName = Path.GetFileName(file.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
                var relativePath = Path.Combine("/App_Data/Images", fileName);
                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);

                list.Add(relativePath);
            }
            TempData["FeaturedImage"] = list;
            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult DeleteImage(string[] fileNames)
        {
            var attachments = TempData["FeaturedImage"] as List<string>;
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                }

                if (attachments != null)
                    attachments.Remove(Path.Combine("/App_Data/Images", fileName));
            }
            if (attachments != null)
                TempData["FeaturedImage"] = attachments;

            // Return an empty string to signify success
            return Content("");
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> attachments)
        {
            var list = TempData["Attachments"] as List<string>;
            if (list == null)
                list = new List<string>();
            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var fileName = Path.GetFileName(file.FileName);
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data/Files"), fileName);
                var relativePath = Path.Combine("/App_Data/Files", fileName);
                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);

                list.Add(relativePath);
            }
            TempData["Attachments"] = list;
            // Return an empty string to signify success
            return Content("");
        }
        public ActionResult DeleteFiles(string[] fileNames)
        {
            var attachments = TempData["Attachments"] as List<string>;
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data/Files"), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                }

                if (attachments != null)
                    attachments.Remove(Path.Combine("/App_Data/Files", fileName));
            }
            if (attachments != null)
                TempData["Attachments"] = attachments;

            // Return an empty string to signify success
            return Content("");
        }

        protected void Delete(string map, string[] fileNames)
        {
            var attachments = TempData["Attachments"] as List<string>;
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath(map), fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
