using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

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


        public ActionResult Categories()
        {
            List<CategoryBM> categories;
            using (CategoryBusinessManager bm = new CategoryBusinessManager())
            {
                categories = bm.Get();
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(new CategoryBM(null));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddCategory(CategoryBM bm)
        {
            using (CategoryBusinessManager businessManager = new CategoryBusinessManager())
            {
                try
                {
                    int inserted = businessManager.Add(bm);
                    TempData["CategorySuccessMessage"] = "Category "+inserted.ToString()+" Has Been Added!";
                    return RedirectToAction("Categories");

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddCategoryError",e.StackTrace);
                    return AddCategory(bm);

                }

            }
        }
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            return View(new CategoryBM(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateCategory(CategoryBM bm)
        {
            using (CategoryBusinessManager businessManager = new CategoryBusinessManager())
            {
                try
                {
                    businessManager.Update(bm);
                    TempData["CategorySuccessMessage"] = "Category Has Been Updated!";
                    return RedirectToAction("Categories");

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddCategoryError", e.StackTrace);
                    return AddCategory(bm);

                }

            }
        }

        public ActionResult DeleteCategory(int id)
        {
            using (CategoryBusinessManager businessManager = new CategoryBusinessManager())
            {
                try
                {
                    businessManager.Delete(new CategoryBM(id));
                    TempData["CategorySuccessMessage"] = "Category Has Been Deleted!";
                    return RedirectToAction("Categories");

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddCategoryError", e.StackTrace);

                }

            }
            return RedirectToAction("Categories");
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
            int ATTIBUTEMAXCOUNT = 30;
            for (int i = 0; i < ATTIBUTEMAXCOUNT; i++)
            {
                productBm.ProductAttributes.Add(new ProductAttribute());
            }
            productBm.ProductPrices = new List<ProductPrice>();
            using (SegmentManager m = new SegmentManager())
            {
                var segments = m.GetAll();
                foreach (var segment in segments)
                {

                    productBm.ProductPrices.Add(new ProductPrice() { Segment = segment.SegmentName });
                    productBm.ProductDiscounts.Add(new ProductDiscount() { Segment = segment.SegmentName });


                }
            }
            return View(productBm);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductBM bm)
        {
            using (ProductBusinessManager productManager = new ProductBusinessManager())
            {
                try
                {
                    TempData["AddSuccessMessage"] = "Ürün Başarılı bir şekilde eklendi";
                    productManager.Add(bm);
                    return RedirectToAction("Products");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddError","Veri eklenirken veritabanında bir hata meydana geldi.");
                    return AddProduct(bm);

                }
            }
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
        public ActionResult AddCustomer(CustomerBM customerModel)
        {

            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                manager.Add(customerModel);
            }

            TempData["Message"] = "Customer Added!";
            return RedirectToAction("Customers", "Admin");

        }

        public ActionResult DeleteCustomer(int id)
        {
            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                manager.Delete(manager.GetById(id));
            }
            
            return RedirectToAction("Customers", "Admin");

        }

        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            return View(new CustomerBM(id));
        }
        [HttpPost]
        public ActionResult UpdateCustomer(CustomerBM customerModel)
        {

            using (CustomerBusinessManager manager = new CustomerBusinessManager())
            {
                manager.Update(customerModel);
            }
            TempData["Message"] = "Customer Updated!";
            return RedirectToAction("Customers", "Admin");

        }

        [HttpGet]
        public ActionResult AddAttributeGroup()
        {
            return View(new AttributeGroupBM(null));
        }
        [HttpPost]
        public ActionResult AddAttributeGroup(AttributeGroupBM attributeGroupModel)
        {
            using (AttributeGroupBusinessManager manager = new AttributeGroupBusinessManager())
            {
                manager.Add(attributeGroupModel);
            }
            TempData["MessageAttributeGroup"] = "Attribute Group Added!";
            return RedirectToAction("AttributeGroupList", "Admin");
        }

        public ActionResult AttributeGroupList()
        {
            List<AttributeGroupBM> attributeGroupList;
            using (AttributeGroupBusinessManager manager = new AttributeGroupBusinessManager())
            {
                attributeGroupList =  manager.Get();
            }
            return View(attributeGroupList);
        }

        public ActionResult DeleteAttributeGroup(int id)
        {
            using (AttributeGroupBusinessManager manager = new AttributeGroupBusinessManager())
            {
                manager.Delete(manager.GetById(id));
            }
            return RedirectToAction("AttributeGroupList", "Admin");
        }

        public ActionResult AttributeList()
        {
            List<AttributeBM> allAttributes;
            using (AttributeBusinessManager manager = new AttributeBusinessManager())
            {
                allAttributes = manager.Get();
            }
            return View(allAttributes);
        }
        
    }
}