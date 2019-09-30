using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.ControllerModels.Client;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.ControllerModels.Client.ClientFilters;

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

        public ActionResult Category(int id,int page=0,CategoryFilters filters=null)
        {
            Category category;
            category = new Category(id, page,filters);
            
            return View(category);
        }

      
    
    }
}