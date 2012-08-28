using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication.Controllers
{
    public class DownloadController : BaseController
    {
        //
        // GET: /Download/

        [HttpGet]
        public ActionResult Index(string id)
        {
            var file = db.Files.Where(e => e.Location == id).FirstOrDefault();
            if (file == null)
            {
                return View();
            }
            string path = Server.MapPath("~/App_Data/Files/" + file.Name);
            if (System.IO.File.Exists(path))
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=\""+ file.Name +"\"");
                Response.AddHeader("Content-Type", "application/force-download");
                return File(path, Common.GetMimeType(file.Name));
            }
            return View();
        }

    }
}
