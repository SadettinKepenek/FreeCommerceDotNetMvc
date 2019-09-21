using FreeCommerceDotNet.Models.BusinessManager;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace FreeCommerceDotNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string p1)
        {
            using (ProductBusinessManager m = new ProductBusinessManager())
            {
                var result = m.DeleteEntry(16);
                if (!result.removable)
                    result.msg.ForEach(x => Debug.WriteLine(x));

            }
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