using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc.UI;
using System.IO;

namespace WebApplication.Controllers
{

    public class FileBrowserController : EditorFileBrowserController
    {
        private const string contentFolderRoot = "~/App_Data/Images";
        private const string prettyName = "Entries";
        private static readonly string[] foldersToCopy = new[] { "~/App_Data/Images/Entries" };


        public ActionResult DisplayImage(string filename)
        {
            var path = Server.MapPath(Path.Combine("~/App_Data/Images", filename));
            if (System.IO.File.Exists(path))
            {
                return File(path, Common.GetMimeType(path));
            }
            return null;
        }

        public ActionResult DisplayEntryImage(string filename)
        {
            var path = Server.MapPath(Path.Combine("~/App_Data/Images/Entries", filename));
            if (System.IO.File.Exists(path))
            {
                return File(path, Common.GetMimeType(path));
            }
            return null;
        }

        private string CreateUserFolder()
        {
            var virtualPath = Path.Combine(contentFolderRoot, prettyName);
            var path = Server.MapPath(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                foreach (var sourceFolder in foldersToCopy)
                {
                    CopyFolder(Server.MapPath(sourceFolder), path);
                }
            }
            return virtualPath;
        }
        private void CopyFolder(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            foreach (var file in Directory.EnumerateFiles(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(file));
                System.IO.File.Copy(file, dest);
            }
            foreach (var folder in Directory.EnumerateDirectories(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(folder));
                CopyFolder(folder, dest);
            }
        }

        public override string[] ContentPaths
        {
            get { return new[] { CreateUserFolder() }; }
        }
    }
}

