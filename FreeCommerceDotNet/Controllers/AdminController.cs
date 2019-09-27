using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using System;
using System.Collections.Generic;
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
                    TempData["CategorySuccessMessage"] = "Category " + inserted.ToString() + " Has Been Added!";
                    return RedirectToAction("Categories");

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddCategoryError", e.StackTrace);
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


        public ActionResult ShippingGateways()
        {

            using (ShippingBusinessManager bm = new ShippingBusinessManager())
            {
                return View(bm.Get());

            }
        }

        [HttpGet]
        public ActionResult AddShippingGateway()
        {
            return View(new ShippingBM(null));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddShippingGateway(ShippingBM bm)
        {
            try
            {
                using (ShippingBusinessManager businessManager = new ShippingBusinessManager())
                {
                    businessManager.Add(bm);
                }
                TempData["ShippingGatewayMessage"] = "Gateway Has Been Added.";

            }
            catch (Exception e)
            {
                TempData["ShippingGatewayMessage"] = "Database Insert Error.";

            }
            return RedirectToAction("ShippingGateways");
        }
        [HttpGet]
        public ActionResult UpdateShippingGateway(int id)
        {
            return View(new ShippingBM(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateShippingGateway(ShippingBM bm)
        {
            try
            {
                using (ShippingBusinessManager businessManager = new ShippingBusinessManager())
                {
                    businessManager.Update(bm);
                }
                TempData["ShippingGatewayMessage"] = "Gateway Has Been Updated.";

            }
            catch (Exception e)
            {
                TempData["ShippingGatewayMessage"] = "Database Update Error." + e.StackTrace;

            }
            return RedirectToAction("ShippingGateways");
        }
        public ActionResult DeleteShippingGateway(int id)
        {
            try
            {
                using (PaymentBusinessManager businessManager = new PaymentBusinessManager())
                {
                    businessManager.Delete(new PaymentBM(id));
                }
                TempData["ShippingGatewayMessage"] = "Gateway Has Been Deleted.";

            }
            catch (Exception e)
            {
                TempData["ShippingGatewayMessage"] = "Database Delete Error." + e.StackTrace;

            }
            return RedirectToAction("ShippingGateways");
        }







        public ActionResult PaymentGateways()
        {
            using (PaymentBusinessManager bm = new PaymentBusinessManager())
            {
                return View(bm.Get());

            }
        }

        [HttpGet]
        public ActionResult AddPaymentGateway()
        {
            return View(new PaymentBM(null));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddPaymentGateway(PaymentBM bm)
        {
            try
            {
                using (PaymentBusinessManager businessManager = new PaymentBusinessManager())
                {
                    businessManager.Add(bm);
                }
                TempData["PaymentGatewayMessage"] = "Gateway Has Been Added.";

            }
            catch (Exception e)
            {
                TempData["PaymentGatewayMessage"] = "Database Insert Error.";

            }
            return RedirectToAction("PaymentGateways");
        }

        [HttpGet]
        public ActionResult UpdatePaymentGateway(int id)
        {
            return View(new PaymentBM(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdatePaymentGateway(PaymentBM bm)
        {
            try
            {
                using (PaymentBusinessManager businessManager = new PaymentBusinessManager())
                {
                    businessManager.Update(bm);
                }
                TempData["PaymentGatewayMessage"] = "Gateway Has Been Updated.";

            }
            catch (Exception e)
            {
                TempData["PaymentGatewayMessage"] = "Database Update Error." + e.StackTrace;

            }
            return RedirectToAction("PaymentGateways");
        }

        public ActionResult DeletePaymentGateway(int id)
        {
            try
            {
                using (PaymentBusinessManager businessManager = new PaymentBusinessManager())
                {
                    businessManager.Delete(new PaymentBM(id));
                }
                TempData["PaymentGatewayMessage"] = "Gateway Has Been Deleted.";

            }
            catch (Exception e)
            {
                TempData["PaymentGatewayMessage"] = "Database Delete Error." + e.StackTrace;

            }
            return RedirectToAction("PaymentGateways");
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
                    ModelState.AddModelError("AddError", "Veri eklenirken veritabanında bir hata meydana geldi.");
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
                attributeGroupList = manager.Get();
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
        [HttpGet]
        public ActionResult UpdateAttributeGroup(int id)
        {
            return View(new AttributeGroupBM(id));
        }
        [HttpPost]
        public ActionResult UpdateAttributeGroup(AttributeGroupBM attributeGroupModel)
        {
            using (AttributeGroupBusinessManager manager = new AttributeGroupBusinessManager())
            {
                manager.Update(attributeGroupModel);
            }
            TempData["MessageAttributeGroup"] = "Attribute Group Updated!";
            return RedirectToAction("AttributeGroupList", "Admin");
        }

        [HttpGet]
        public ActionResult UpdateAttribute(int id)
        {
            return View(new AttributeBM(id));
        }
        [HttpPost]
        public ActionResult UpdateAttribute(AttributeBM attributeModel)
        {
            using (AttributeBusinessManager manager = new AttributeBusinessManager())
            {
                manager.Update(attributeModel);
            }
            TempData["MessageAttribute"] = "Attribute Updated!";
            return RedirectToAction("AttributeList", "Admin");
        }
        public ActionResult DeleteAttribute(int id)
        {
            using (AttributeBusinessManager manager = new AttributeBusinessManager())
            {
                manager.Delete(manager.GetById(id));
            }

            return RedirectToAction("AttributeList", "Admin");
        }

        [HttpGet]
        public ActionResult AddAttribute()
        {
            return View(new AttributeBM(null));
        }
        [HttpPost]
        public ActionResult AddAttribute(AttributeBM attributeModel)
        {
            using (AttributeBusinessManager manager = new AttributeBusinessManager())
            {
                manager.Add(attributeModel);
            }
            TempData["MessageAttribute"] = "Attribute Added!";
            return RedirectToAction("AttributeList", "Admin");
        }
        [HttpGet]
        public ActionResult Reviews()
        {
            List<ReviewBM> reviews;
            using (ReviewBusinessManager manager = new ReviewBusinessManager())
            {
                reviews = manager.Get();
            }
            return View(reviews);
        }

        public ActionResult DeleteReview(int id)
        {
            using (ReviewBusinessManager manager = new ReviewBusinessManager())
            {
                manager.Delete(manager.GetById(id));
            }
            return RedirectToAction("Reviews", "Admin");
        }

        public ActionResult ChangeReviewVisibility(int id)
        {

            using (ReviewBusinessManager manager = new ReviewBusinessManager())
            {
                var result = manager.GetById(id);
                result.Reviews.Status = !result.Reviews.Status;
                manager.Update(result);
            }

            return RedirectToAction("Reviews", "Admin");
        }

        public ActionResult Returns()
        {
            using (OrderReturnBusinessManager bm = new OrderReturnBusinessManager())
            {
                return View(bm.Get());
            }
        }

        [HttpGet]
        public ActionResult AddReturn()
        {
            return View(new OrderReturnBM(null));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddReturn(OrderReturnBM returnBm)
        {
            using (OrderReturnBusinessManager bm = new OrderReturnBusinessManager())
            {
                bm.Add(returnBm);
                TempData["OrderReturnSuccessMessage"] = "Has Been Added";
                return RedirectToAction("Returns");
            }
        }

        [HttpGet]
        public ActionResult UpdateReturn(int id)
        {
            return View(new OrderReturnBM(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateReturn(OrderReturnBM returnBm)
        {
            using (OrderReturnBusinessManager bm = new OrderReturnBusinessManager())
            {
                bm.Update(returnBm);
                TempData["OrderReturnSuccessMessage"] = "Has Been Updated";
                return RedirectToAction("Returns");
            }
        }

        [HttpPost]
        public ActionResult DeleteReturn(int id)
        {
            using (OrderReturnBusinessManager bm = new OrderReturnBusinessManager())
            {
                bm.Delete(new OrderReturnBM(id));
                TempData["OrderReturnSuccessMessage"] = "Has Been Deleted";
                return RedirectToAction("Returns");
            }
        }


        public ActionResult Users()
        {
            using (UsersBusinessManager bm = new UsersBusinessManager())
            {
                return View(bm.Get());
            }
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new UsersBM(null));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddUser(UsersBM bm)
        {
            using (UsersBusinessManager manager=new UsersBusinessManager())
            {
                manager.Add(bm);
            }

            TempData["UserSuccessMessage"] = "Success !";
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            return View(new UsersBM(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateUser(UsersBM bm)
        {
            using (UsersBusinessManager manager = new UsersBusinessManager())
            {
                manager.Update(bm);
            }
            TempData["UserSuccessMessage"] = "Success !";
            return RedirectToAction("Users");
        }

        public ActionResult DeleteUser(int id)
        {
            using (UsersBusinessManager manager = new UsersBusinessManager())
            {
                manager.Delete(new UsersBM(id));
            }
            TempData["UserSuccessMessage"] = "Success !";
            return RedirectToAction("Users");
        }


        public ActionResult Orders()
        {
            using (OrderMasterBusinessManager bm = new OrderMasterBusinessManager())
            {
                return View(bm.Get());
            }
        }

        public ActionResult OrderDetail(int id)
        {

            var orderMasterBm = new OrderMasterBM(null);
            for (int i = 0; i <= 1; i++)
            {
                orderMasterBm.OrderDetails.Add(new OrderDetail());
            }
            return View(orderMasterBm);

        }

        [HttpGet]
        public ActionResult AddOrder()
        {
            var orderMasterBm = new OrderMasterBM(null);
            for (int i = 0; i <= 1; i++)
            {
                orderMasterBm.OrderDetails.Add(new OrderDetail());
            }
            return View(orderMasterBm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddOrder(OrderMasterBM bm)
        {

            using (OrderMasterBusinessManager manager = new OrderMasterBusinessManager())
            {
                manager.Add(bm);
                TempData["OrderMasterMessage"] = "Order Has Been Added";
            }
            return RedirectToAction("Orders");
        }

        [HttpGet]
        public ActionResult UpdateOrder(int id)
        {
            return View(new OrderMasterBM(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateOrder(OrderMasterBM bm)
        {
            using (OrderMasterBusinessManager manager = new OrderMasterBusinessManager())
            {
                manager.Update(bm);
                TempData["OrderMasterMessage"] = "Order Has Been Updated";
            }
            return RedirectToAction("Orders");
        }

        public ActionResult DeleteOrder(int id)
        {
            using (OrderMasterBusinessManager manager = new OrderMasterBusinessManager())
            {
                manager.Delete(new OrderMasterBM(id));
                TempData["OrderMasterMessage"] = "Order Has Been Deleted";
            }
            return RedirectToAction("Orders");
        }
    }
}