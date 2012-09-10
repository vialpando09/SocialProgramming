using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropNet;

namespace WebApplication.Controllers
{
    public class DropBoxController : BaseController
    {
        //
        // GET: /DropBox/

        public ActionResult File(string id)
        {
            string filename = db.Files.Where(e => e.Location == id).Select(e => e.Name).FirstOrDefault();

            if (string.IsNullOrEmpty(filename))
                return Content("");

            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            var file = _client.GetFile("/Public/Files/" + filename);
            
            Response.AddHeader("Content-Disposition", "attachment; filename=\""+ filename +"\"");

            return File(file, Common.GetMimeType(filename));
        }

        public ActionResult Image(string filename)
        {
            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            var file = _client.GetFile("/Public/Images/" + filename);
            
            return File(file, Common.GetMimeType(filename));
        }

    }
}
