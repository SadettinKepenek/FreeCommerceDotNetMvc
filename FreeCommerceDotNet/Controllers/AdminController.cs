using System.Collections.Generic;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
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

        [HttpGet]
        public ActionResult DeleteCustomer()
        {
            List<CustomerBM> customers;
            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                customers = manager.Get();
            }
            return View(customers);
        }
        [HttpPost]
        public ActionResult DeleteCustomer(int id)
        {
            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                manager.Delete(manager.GetById(id));
            }
            return View();
        }
        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                return View(manager.GetById(id));
            }

        }
        [HttpPost]
        public ActionResult UpdateCustomer(CustomerBM customer)
        {

            return View();
        }
    }
}