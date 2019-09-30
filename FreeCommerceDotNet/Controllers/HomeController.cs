using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace FreeCommerceDotNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string p1)
        {
            return View();
        }
        
        // Develop Branch
        public ActionResult Product(int id)
        {          
            var productModel = new ProductBM(id);
            return View(productModel);
        }
    }
}