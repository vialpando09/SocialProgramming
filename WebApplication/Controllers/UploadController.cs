using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DropNet;

namespace WebApplication.Controllers
{
    public class UploadController : BaseController
    {
        //        
        // GET: /Upload/

        private string path = "/Public/Files/";

        public string PathValue { set { path = value; } }

        [LoginAuthorize]
        public ActionResult SaveFeatured(IEnumerable<HttpPostedFileBase> featuredImage)
        {
            path = "/Public/Images/";
            List<string> list = new List<string>();
            foreach (var file in featuredImage)
            {
                var fileName = Path.GetFileName(file.FileName);
                var relativePath = path + fileName;
                list.Add(relativePath);
            }
            TempData["FeaturedImage"] = list;
            return Save(featuredImage);
        }

        [LoginAuthorize]
        public ActionResult RemoveFeatured(string[] fileNames)
        {
            path = "/Public/Images/";
            var attachments = TempData["FeaturedImage"] as List<string>;
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var relativePath = path + fileName;

                if (attachments != null)
                    attachments.Remove(relativePath);
            }
            if (attachments != null)
                TempData["FeaturedImage"] = attachments;
            
            return Remove(fileNames);
        }

        [LoginAuthorize]
        public ActionResult SaveFiles(IEnumerable<HttpPostedFileBase> attachments)
        {
            path = "/Public/Files/";
            var list = TempData["Attachments"] as List<string>;
            if (list == null)
                list = new List<string>();

            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                var fileName = Path.GetFileName(file.FileName);
                var relativePath = path + fileName;                
                list.Add(relativePath);
            }
            TempData["Attachments"] = list;

            return Save(attachments);
        }

        [LoginAuthorize]
        public ActionResult RemoveFiles(string[] fileNames)
        {
            path = "/Public/Files/";
            var attachments = TempData["Attachments"] as List<string>;
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var relativePath = path + fileName;                
                
                if (attachments != null)
                    attachments.Remove(relativePath);
            }
            if (attachments != null)
                TempData["Attachments"] = attachments;

            return Remove(fileNames);
        }

        [LoginAuthorize]
        public ActionResult Save(IEnumerable<HttpPostedFileBase> attachments)
        {
            // The Name of the Upload component is "attachments" 
            foreach (var file in attachments)
            {
                var fileName = Path.GetFileName(file.FileName);

                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();

                var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
                _client.UploadFile(path, fileName, data);
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
                var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
                try
                {
                    _client.Delete(path + fullName);
                }
                catch (DropNet.Exceptions.DropboxException)
                {
                }
                return Content("");
            }
            // Return an empty string to signify success
            return Content("");
        }
    }
}
