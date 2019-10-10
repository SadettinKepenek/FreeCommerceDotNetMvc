using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.ControllerModels.Client.ClientFilters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        public ActionResult Category(int id, int page = 0, CategoryFilters filters = null)
        {
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
            if (filters != null)
            {
                if (filters.AltKategoriId != 0)
                {
                    productsList = productsList.Where(x => x.CategoryId == filters.AltKategoriId);
                }

                if (filters.BrandId != 0)
                {
                    productsList = productsList.Where(x => x.Brand == filters.BrandId);
                }

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



    }
}