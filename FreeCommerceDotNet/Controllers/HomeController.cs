using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string p1)
        {
            // TODO

            Utilities.GetTablesForeignKeys("Products");
            Utilities.GetTablesForeignKeys("Customers");
            return View();
        }
     
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}