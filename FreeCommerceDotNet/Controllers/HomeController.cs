using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.ControllerModels.Client;
using System.Collections.Generic;

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
            
            using (ProductBusinessManager manager = new ProductBusinessManager())
            {
                var products = manager.GetById(id);
                return View(products);
            }
            

        }

        public ActionResult Category(int id,int page=0)
        {
            Category category;
            category = page!=0 ? new Category(id) : new Category(id, page);
            
            return View(category);
        }
    
    }
}