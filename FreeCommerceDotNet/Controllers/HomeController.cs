using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.ControllerModels.Client;
using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.ControllerModels.Client.ClientFilters;
using Product = FreeCommerceDotNet.Entities.Concrete.Product;
using ProductAttributeManager = FreeCommerceDotNet.BLL.Concrete.ProductAttributeManager;
using ProductDiscountManager = FreeCommerceDotNet.BLL.Concrete.ProductDiscountManager;
using ProductDiscount = FreeCommerceDotNet.Entities.Concrete.ProductDiscount;
using ProductPrice = FreeCommerceDotNet.Entities.Concrete.ProductPrice;
using ProductManager = FreeCommerceDotNet.BLL.Concrete.ProductManager;
using ProductPriceManager = FreeCommerceDotNet.BLL.Concrete.ProductPriceManager;
using FreeCommerceDotNet.DAL.Concrete;
using System.Web.Services;

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
            using (ReviewManager manager = new ReviewManager(new ReviewRepository()))
            {
                var reviews = manager.SelectAll();
                reviews = reviews.Where(x => x.ProductId == id).ToList();
                ViewBag.reviews = reviews;
            }
            using (ProductManager manager = new ProductManager(new ProductRepository()))
            {
                var product = manager.SelectById(id);
                // ProductDiscount productDiscount = new ProductDiscount();
                ViewBag.ProductId = id;
                return View(product);
            }

        }

        public ActionResult Category(int id,int page=0,CategoryFilters filters=null)
        {
            Category category;
            category = new Category(id, page,filters);
            
            return View(category);
        }

      
        public JsonResult AddReview(Reviews review)
        {
            using (ReviewManager manager=new ReviewManager(new ReviewRepository()))
            {
                try
                {
                    var result=manager.Insert(review);
                    return Json(result.Message);
                }
                catch (Exception e)
                {
                    return Json("Failed");
                }
            }
        }
    }
}