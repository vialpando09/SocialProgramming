using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication.Controllers
{
    public class UploadController : BaseController
    {
        //        
        // GET: /Upload/
        
        [LoginAuthorize]
        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachments)
        {
            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                // Some browsers send file names with full path. This needs to be stripped.
                var path = Server.MapPath("~/App_Data");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "Files");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var subFolder = DateTime.Now.Year.ToString() +" "+ DateTime.Now.Month.ToString() +" "+ DateTime.Now.Day.ToString();
                path = Path.Combine(path, subFolder);
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = Path.GetFileName(file.FileName);
                var physicalPath = Path.Combine(path, fileName);

                // The files are not actually saved in this demo
                file.SaveAs(physicalPath);
            }
            // Return an empty string to signify success
            return Content("");
        }

        [LoginAuthorize]
        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var subFolder = DateTime.Now.Year.ToString() + " " + DateTime.Now.Month.ToString() + " " + DateTime.Now.Day.ToString();
                var physicalPath = Path.Combine(Server.MapPath("~/App_Data/Files"),subFolder, fileName);

                // TODO: Verify user permissions
                if (System.IO.File.Exists(physicalPath))
                {
                    // The files are not actually removed in this demo
                    System.IO.File.Delete(physicalPath);
                }
            }
            // Return an empty string to signify success
            return Content("");
        }
    }
}
