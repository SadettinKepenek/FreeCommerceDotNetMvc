using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Product = FreeCommerceDotNet.Entities.Concrete.Product;
using ProductManager = FreeCommerceDotNet.BLL.Concrete.ProductManager;

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

        public ActionResult Category(int id, int page = 0)
        {
            HttpCookie categoryFilterCookie = HttpContext.Request.Cookies.Get("CategoryFilterId");
            HttpCookie brandFilterCookie = HttpContext.Request.Cookies.Get("BrandFilterId");
            HttpCookie minPriceCookie = HttpContext.Request.Cookies.Get("MinPrice");
            HttpCookie maxPriceCookie = HttpContext.Request.Cookies.Get("MaxPrice");

            ViewBag.CategoryId = id;
            

            int PRODUCTCOUNT = 12;
            page = page != 0 ? page : 1;

            CategoryManager manager = new CategoryManager(new CategoryRepository());
            var category = manager.SelectById(id);
            IEnumerable<Product> productsList = category.Products;

            #region ApplyPagination

            if (productsList.Count()>PRODUCTCOUNT)
            {
                productsList = productsList.Skip(page * PRODUCTCOUNT).Take(PRODUCTCOUNT);
            }


            #endregion

            #region ApplyFilters 

            if (categoryFilterCookie != null && !String.IsNullOrEmpty(categoryFilterCookie.Value))
            {
                int categoryId = Convert.ToInt32(Server.HtmlEncode(categoryFilterCookie.Value));
                productsList = productsList.Where(x => x.CategoryId == categoryId);
 
            }

            if (brandFilterCookie != null && !String.IsNullOrEmpty(brandFilterCookie.Value))
            {
                int brandId = Convert.ToInt32(Server.HtmlEncode(brandFilterCookie.Value));
                productsList = productsList.Where(x => x.Brand == brandId);
              
            }

            if (minPriceCookie!=null && maxPriceCookie!=null && !String.IsNullOrEmpty(minPriceCookie.Value) && !String.IsNullOrEmpty(maxPriceCookie.Value))
            {

                double minPrice = Convert.ToDouble(Server.HtmlEncode(minPriceCookie.Value));
                double maxPrice = Convert.ToDouble(Server.HtmlEncode(maxPriceCookie.Value));
                productsList = from product in productsList
                    where product.ProductPrices.Any(x => x.Price >= minPrice && x.Price <= maxPrice)
                    select product;

            

            }
            #endregion

            #region ToList

            category.Products = productsList.ToList();

            #endregion


            #region FindMaxPage

            var pageCount = (int)category.Products.Count / PRODUCTCOUNT;
            ViewBag.MaxPage = pageCount == 0 ? 1:pageCount;
            
            ViewBag.Page = page;

            #endregion




            return View(category);
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("ErrorRedirect");
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