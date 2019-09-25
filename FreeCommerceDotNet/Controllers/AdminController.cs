using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            List<ProductBM> products;
            using (ProductBusinessManager manager = new ProductBusinessManager())
            {
                products = manager.Get();
            }
            return View(products);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            var productBm = new ProductBM(null);
            int ATTIBUTEMAXCOUNT = 10;
            for (int i = 0; i < ATTIBUTEMAXCOUNT; i++)
            {
                productBm.ProductAttributes.Add(new ProductAttribute());
            }

            using (SegmentManager m = new SegmentManager())
            {
                var segments = m.GetAll();
                foreach (var segment in segments)
                {
                    productBm.ProductPrices.Add(new ProductPrice(){Segment = segment.SegmentName});
                    productBm.ProductDiscounts.Add(new ProductDiscount(){Segment = segment.SegmentName});


                }
            }
            return View(productBm);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductBM bm)
        {
            return View();
        }



        public ActionResult Customers()
        {
            List<CustomerBM> customers;
            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                customers = manager.Get();
            }
            return View(customers);
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(CustomerBM bm)
        {
            return View();
        }

        public ActionResult DeleteCustomer(int id)
        {
            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                manager.Delete(manager.GetById(id));
            }

            return RedirectToAction("Customers", "Admin", null);
        }

        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            return View(new CustomerBM(id));
        }
        [HttpPost]
        public ActionResult UpdateCustomer(CustomerBM customer)
        {
            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                manager.Update(customer);
            }
            return View("Customers");
        }
    }
}