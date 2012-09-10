using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Drawing;
using Amazon.S3.Model;
using Amazon.S3;
using DropNet;
using System.Net;

namespace WebApplication.Controllers
{
    public class FileBrowserController : BaseController, IImageBrowserController
    {
        //
        // GET: /FileBrowserTest/

        public string[] ContentPaths
        {
            get { return new[] { "/Public" }; }
        }

        public JsonResult Browse(string path)
        {
            path = Path.Combine(ContentPaths[0], path).Replace('\\', '/') + "/";

            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            var _metaData = _client.GetMetaData(path);

            BrowseResult result = new BrowseResult();
            result.ContentPaths = ContentPaths;
            result.Path = path;

            result.Files = _metaData.Contents.Where(e => e.Extension != "").Select(e => 
                {
                    long size = 0;
                    if (e.Size.Contains(" B"))
                        size = (long)double.Parse(e.Size.Replace(" B", "").Replace('.', ','));
                    else if( e.Size.Contains(" KB"))
                        size = (long)(double.Parse(e.Size.Replace(" KB", "").Replace('.', ',')) * 1024);
                    else if (e.Size.Contains(" MB"))
                        size = (long)(double.Parse(e.Size.Replace(" MB", "").Replace('.', ',')) * 1024 * 1024);
                    else if (e.Size.Contains(" GB"))
                        size = (long)(double.Parse(e.Size.Replace(" GB", "").Replace('.', ',')) * 1024 * 1024 * 1024);
                    return new FileEntry { Name = e.Name, Size = size };
                });
            result.Directories = _metaData.Contents.Where(e => e.Extension == "").Select(e => new DirectoryEntry { Name = e.Name });

            return this.Json(result);
        }

        public ActionResult CreateDirectory(string path, string name)
        {
            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            path = Path.Combine(path, name).Replace('\\', '/');
            _client.CreateFolder(path);
            return Content("");
        }

        public ActionResult DeleteDirectory(string path)
        {
            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            _client.Delete(path);
            return Content("");
        }

        public ActionResult DeleteFile(string path)
        {
            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            _client.Delete(path);
            return Content("");
        }

        public ActionResult Thumbnail(string path)
        {
            
            var result = CreateBigThumbNail(86, path);
            return result;
        }

        private FileContentResult CreateBigThumbNail(int newWidth, string path)
        {
            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            var _file = _client.GetFile(path);
            MemoryStream myStream = new MemoryStream();
            myStream.Write(_file, 0, _file.Length);
            using (Bitmap originalImage = new Bitmap(myStream))
            {
                int width = newWidth;
                int height = (originalImage.Height * newWidth) / originalImage.Width;
                Bitmap thumbnail = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)thumbnail))
                    g.DrawImage(originalImage, 0, 0, width, height);

                ImageConverter converter = new ImageConverter();
                return File((byte[])converter.ConvertTo(thumbnail, typeof(byte[])), Common.GetMimeType(path));
            }
        }

        public ActionResult Upload(string path, HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);

            MemoryStream target = new MemoryStream();
            file.InputStream.CopyTo(target);
            byte[] data = target.ToArray();

            var _client = new DropNetClient(accessKey, secretAccessKey, userTokenKey, userSecretKey);
            _client.UploadFile(path, fileName, data);

            return this.Json(new FileEntry { Name = fileName, Size = file.ContentLength });
        }
    }
}
