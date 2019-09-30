using FreeCommerceDotNet.Models.BusinessManager;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.ControllerModels.Client;

namespace FreeCommerceDotNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string p1)
        {
            return View();
        }
        
        // Develop Branch
        public ActionResult Product()
        {

            return View();
        }

        public ActionResult Category(int id,int page=0)
        {
            Category category;
            category = page!=0 ? new Category(id) : new Category(id, page);
            
            return View(category);
        }
    
    }
}