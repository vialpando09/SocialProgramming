namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Telerik.Web.Mvc.Examples.Models;
    using System.Collections.Generic;

    public partial class GridController : Controller
    {
        public ActionResult ColumnResizing(IDictionary<string,int> config)
        {
            if (config == null)
            {
                config = new Dictionary<string, int>
                {
                    {"GridWidth", 0},
                    {"OrderIDWidth", 100},
                    {"ContactNameWidth", 200},
                    {"ShipAddressWidth", 450},
                    {"OrderDateWidth", 130}
                };
            }

            ViewData["config"] = config;
            
            return View(GetOrderDto());
        }

        [GridAction]
        public ActionResult _ColumnResizing()
        {
            return View(new GridModel(GetOrderDto()));
        }
    }
}