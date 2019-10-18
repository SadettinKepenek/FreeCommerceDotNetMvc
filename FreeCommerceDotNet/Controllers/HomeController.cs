using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Product = FreeCommerceDotNet.Entities.Concrete.Product;
using ProductManager = FreeCommerceDotNet.BLL.Concrete.ProductManager;
using OrderManager = FreeCommerceDotNet.BLL.Concrete.OrderMasterManager;

using ProductPriceManager = FreeCommerceDotNet.BLL.Concrete.ProductPriceManager;
using FreeCommerceDotNet.DAL.Concrete;
using System.Web.Services;
using System.Diagnostics;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.Models;
using Newtonsoft.Json;
using FuzzyString;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Product> discountedProducts;
            List<Product> newProducts;
            
            using (ProductManager manager = new ProductManager(new ProductRepository()))
            {
                newProducts = manager.SelectAll();
                for (int i = 0; i < newProducts.Count-1; i++)
                {
                    DateTime dt = DateTime.Now;
                    var productDate = Convert.ToDateTime(newProducts[i].AvailableDate);
                    var diff = dt - productDate;
                    if (diff.TotalDays > 8)
                    {
                        newProducts.Remove(newProducts[i]);
                    }
                }
                if (newProducts != null)
                {
                    ViewBag.NewProducts = newProducts;
                }
                         
                discountedProducts = manager.SelectByFilter(new List<DBFilter>()
                {
                    new DBFilter()
                    {
                        ParamName = "@JustInDiscount",
                        ParamValue = true
                    }

                });
                
                ViewBag.SpecialOffer = discountedProducts.Take(1).ToList().FirstOrDefault();
                
                using (OrderMasterManager orderManager = new OrderMasterManager(new OrderMasterRepository()))
                {
                    var allOrders = orderManager.SelectAll();
                    var productIds = allOrders.Select(x => x.OrderDetails.FirstOrDefault().ProductId);
                    var bestSellerProducts = productIds.GroupBy(y => y).OrderByDescending(g => g.Count()).Take(4).Select(g => g.Key);
                    
                    if (bestSellerProducts != null)
                    {
                        List<Product> products = new List<Product>();
                        foreach (var item in bestSellerProducts)
                        {                         
                            products.Add(manager.SelectById(item));                           
                        }
                        ViewBag.TrendProducts = products;
                    }                 
                    
                }
            }
            if (discountedProducts != null)
                return View(discountedProducts);
            else
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
                //kullanıcı giriş yapmış ve order'ı varsa AddReview seciton'u gözükecek
                //Aynı zamanda kullanıcının bir review'i varsa review ekleyemeyecek
                ViewBag.perm = permReview(id);
            }


            using (ProductManager manager = new ProductManager(new ProductRepository()))
            {
                var product = manager.SelectById(id);
                // ProductDiscount productDiscount = new ProductDiscount();
                ViewBag.ProductId = id;
                return View(product);
            }


        }
        private bool permReview(int productId)
        {
            bool isAuth = User.Identity.IsAuthenticated;
            bool hasReview = false;
            bool perm = false;
            int customerId = 0;
            if (isAuth)
            {
                var userName = User.Identity.Name;
                var user = new CustomerManager(new CustomerRepositorycs()).SelectByFilter(
                    new List<FreeCommerceDotNet.Common.Concrete.DBFilter>() {
                        new FreeCommerceDotNet.Common.Concrete.DBFilter()
                        {
                        ParamName="@username",
                        ParamValue=userName
                        }}
                            ).FirstOrDefault();
                customerId = user.CustomerId;
                List<FreeCommerceDotNet.Common.Concrete.DBFilter> filters = new List<FreeCommerceDotNet.Common.Concrete.DBFilter>();
                filters.Add(new FreeCommerceDotNet.Common.Concrete.DBFilter() { ParamName = "@CustomerId", ParamValue = user.CustomerId });
                var userOrders = new OrderManager(new OrderMasterRepository()).SelectByFilter(filters);
                if (userOrders != null)
                {
                    perm = userOrders.Any(x => x != null && x.OrderDetails != null && x.OrderDetails.Any(y => y != null && y.ProductId == productId));

                }
                using (ReviewManager manager = new ReviewManager(new ReviewRepository()))
                {
                    if (manager.SelectAll().Where(x => x.CustomerId == customerId).ToList().Count != 0)
                        hasReview = true;


                }
                ViewBag.customerId = customerId;
                ViewBag.hasReview = hasReview;
                return perm;
            }
            return perm;
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

            if (productsList.Count() > PRODUCTCOUNT)
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

            if (minPriceCookie != null && maxPriceCookie != null && !String.IsNullOrEmpty(minPriceCookie.Value) && !String.IsNullOrEmpty(maxPriceCookie.Value))
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
            ViewBag.MaxPage = pageCount == 0 ? 1 : pageCount;

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
            using (ReviewManager manager = new ReviewManager(new ReviewRepository()))
            {
                try
                {
                    var result = manager.Insert(review);
                    return Json(result.Message);
                }
                catch (Exception e)
                {
                    return Json("Failed");
                }
            }
        }
        public JsonResult DeleteReview(int ReviewId)
        {
            using (ReviewManager manager = new ReviewManager(new ReviewRepository()))
            {
                try
                {
                    var result = manager.Delete(ReviewId);
                    return Json(result.Message);
                }
                catch (Exception e)
                {
                    return Json("Failed");
                }
            }
        }
        public JsonResult GetSearchResults(string query)
        {
            /// Veritabanı tarafında eğer productname gönderilirse bir kaç filtreleme işleminin yapılması gerek
            /// Belki bunun için ayrıca bir procedüre yazılabilir.
            using (ProductManager manager = new ProductManager(new ProductRepository()))
            {
                try
                {


                    var result = manager.SearchProduct(query);
                    for (int i = 0; i < result.Count - 1; i++)
                    {
                        if (!MatchSourceAndTarget(result[i].ProductName, query))
                        {
                            result.RemoveAt(i);
                        }
                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json("Failed");
                }
            }
        }

        public ActionResult ProductCompare()
        {
            var httpCookie = Request.Cookies.Get("compareList");
            if (httpCookie != null)
            {
                var productCompareJsonModels = JsonConvert.DeserializeObject<List<ProductCompareJSONModel>>(httpCookie.Value);

                var productCompareModel = new ProductCompareModel();
                productCompareModel.Products = new List<Product>();


                using (ProductManager p = new ProductManager(new ProductRepository()))
                {
                    using (ReviewManager m = new ReviewManager(new ReviewRepository()))
                    {
                        for (int i = 0; i < productCompareJsonModels.Count; i++)
                        {
                            int productId = productCompareJsonModels[i].productId;
                            var filters = new List<DBFilter>();
                            filters.Add(new DBFilter() { ParamName = "@ProductId", ParamValue = productId });
                            Product item = p.SelectById(productId);
                            item.Reviews = m.SelectByFilter(filters);
                            productCompareModel.Products.Add(item);
                        }
                    }

                }

                return View(productCompareModel);
            }

            return View();
        }
        private static bool MatchSourceAndTarget(string target, string source)
        {
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Strong;
            bool result = source.ApproximatelyEquals(target, tolerance, options.ToArray());
            return result;
        }
        [HttpGet, ActionName("Changepassword")]
        public ActionResult ChangePassword(int ticketId, Guid token)
        {
            using (UserManager manager = new UserManager(new UserRepository()))
            {
                var ticket = manager.SelectResetTicket(ticketId);
                //token kullanılmışsa
                if (ticket.tokenUsed == true)
                {
                    return RedirectToAction("ForgetPassword", "Security");
                }
                else
                {
                    //token'in süresi geçmişse
                    string today = DateTime.Today.ToString("dd-MM-yyyy");
                    if (!today.Equals(ticket.exprationDate))
                    {
                        return RedirectToAction("ForgetPassword", "Security");
                    }
                    else
                    {
                        ResetTicket resetTicket = new ResetTicket();
                        resetTicket = ticket;
                        resetTicket.tokenUsed = true;
                        manager.UpdateResetTicket(ticketId);
                        return View(manager.SelectById(resetTicket.UserId));
                    }
                }
            }
        }
        [HttpPost]
        public ActionResult ChangePassword(User user)
        {
            using (UserManager manager = new UserManager(new UserRepository()))
            {
                manager.Update(user);
            }
            return RedirectToAction("Index", "Security");
        }

        public ActionResult Faq()
        {
            return View();
        }

    }
}