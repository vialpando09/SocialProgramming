using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class BaseController : Controller, IDisposable
    {
        protected readonly ModelContainer db = new ModelContainer();
        public ModelContainer Db { get { return db; } }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
