using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Amazon.S3.Model;
using Amazon.S3;

namespace WebApplication.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        protected readonly ModelContainer db = new ModelContainer();
        public ModelContainer Db { get { return db; } }

        protected string accessKey = System.Configuration.ConfigurationManager.AppSettings["DropBoxAccessKey"];
        protected string secretAccessKey = System.Configuration.ConfigurationManager.AppSettings["DropBoxSecretKey"];
        protected string userTokenKey = System.Configuration.ConfigurationManager.AppSettings["DropBoxUserTokenKey"];
        protected string userSecretKey = System.Configuration.ConfigurationManager.AppSettings["DropBoxUserSecretKey"];

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
