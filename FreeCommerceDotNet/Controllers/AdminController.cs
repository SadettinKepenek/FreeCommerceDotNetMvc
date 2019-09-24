using System.Collections.Generic;
using System.Web.Mvc;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;

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
            return View(new ProductBM(null));
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