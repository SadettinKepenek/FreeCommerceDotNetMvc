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

    }
}