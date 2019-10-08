using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using OrderDetail = FreeCommerceDotNet.Models.DbModels.OrderDetail;
using Product = FreeCommerceDotNet.Entities.Concrete.Product;
using ProductAttributeManager = FreeCommerceDotNet.BLL.Concrete.ProductAttributeManager;
using ProductDiscountManager = FreeCommerceDotNet.BLL.Concrete.ProductDiscountManager;
using ProductManager = FreeCommerceDotNet.BLL.Concrete.ProductManager;
using ProductPrice = FreeCommerceDotNet.Entities.Concrete.ProductPrice;
using ProductPriceManager = FreeCommerceDotNet.BLL.Concrete.ProductPriceManager;
using SegmentManager = FreeCommerceDotNet.Models.DbManager.SegmentManager;

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
        public ActionResult Categories(bool subCategories)
        {
            List<Category> categories;

            using (CategoryManager categorManager=new CategoryManager(new CategoryRepository()))
            {
                var allCategories = categorManager.SelectAll();

                if (subCategories)
                {
                    categories = new List<Category>();
                    foreach (Category categoryBm in allCategories.Where(x=>x.ParentId!=-1))
                    {
                        categories.Add(categoryBm);
                    }

                    return View(categories);
                }
                else
                {
                    categories = new List<Category>();
                    foreach (Category categoryBm in allCategories.Where(x => x.ParentId == -1))
                    {
                        categories.Add(categoryBm);
                    }

                    return View(categories);
                }
            }
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(new CategoryBM(null));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddCategory(Category bm)
        {
            using (CategoryManager businessManager = new CategoryManager(new CategoryRepository()))
            {
                try
                {

                    int inserted = businessManager.Insert(bm).Id;
                    TempData["CategorySuccessMessage"] = "Category " + inserted.ToString() + " Has Been Added!";
                    return RedirectToAction("Categories",new { subCategories =false});

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddCategoryError", e.StackTrace);
                    return View(bm);

                }

            }
        }
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            return View(new CategoryManager(new CategoryRepository()).SelectById(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateCategory(Category bm)
        {
            using (CategoryManager businessManager = new CategoryManager(new CategoryRepository()))
            {
                try
                {
                    int id=businessManager.Update(bm).Id;
                    TempData["CategorySuccessMessage"] = "Category "+id+" Has Been Updated!";
                    return RedirectToAction("Categories", new { subCategories = false });

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddCategoryError", e.StackTrace);
                    return View(bm);

                }

            }
        }

        public ActionResult DeleteCategory(int id)
        {
            using (CategoryManager businessManager = new CategoryManager(new CategoryRepository()))
            {
                try
                {
                    businessManager.Delete(id);
                    TempData["CategorySuccessMessage"] = "Category Has Been Deleted!";
                    return RedirectToAction("Categories", new { subCategories = false });

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddCategoryError", e.StackTrace);

                }

            }
            return RedirectToAction("Categories", new { subCategories = false });
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
                using (ShippingBusinessManager businessManager = new ShippingBusinessManager())
                {
                    businessManager.Delete(new ShippingBM(id));
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
            List<Product> products;
            using (ProductManager manager = new ProductManager(new ProductRepository()))
            {
                products = manager.SelectAll();
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            var productBm = new Product();
            int ATTIBUTEMAXCOUNT = 1;
            productBm.ProductAttributes=new List<Entities.Concrete.ProductAttribute>();
            productBm.ProductPrices=new List<Entities.Concrete.ProductPrice>();
            productBm.ProductDiscounts=new List<Entities.Concrete.ProductDiscount>();
            for (int i = 0; i < ATTIBUTEMAXCOUNT; i++)
            {
                productBm.ProductAttributes.Add(new Entities.Concrete.ProductAttribute());
            }
            productBm.ProductPrices = new List<Entities.Concrete.ProductPrice>();
            using (SegmentManager m = new SegmentManager())
            {
                var segments = m.GetAll();
                foreach (var segment in segments)
                {

                    productBm.ProductPrices.Add(new Entities.Concrete.ProductPrice() { SegmentId = segment.SegmentId });
                    productBm.ProductDiscounts.Add(new Entities.Concrete.ProductDiscount() { SegmentId = segment.SegmentId });


                }
            }
            return View(productBm);
        }

        [HttpPost]
        public ActionResult AddProduct(Product bm)
        {
            try
            {
                using (ProductManager m = new ProductManager())
                {
                    int insertedId = m.Insert(bm).Id;
                    bm.ProductId = insertedId;
                    using (ProductAttributeManager attributeManager = new ProductAttributeManager(new ProductAttributeRepository()))
                    {
                        foreach (var bmProductAttribute in bm.ProductAttributes)
                        {
                            bmProductAttribute.ProductId = bm.ProductId;
                            attributeManager.Insert(bmProductAttribute);
                        }
                    }

                    using (ProductDiscountManager discountManager = new ProductDiscountManager(new ProductDiscountRepository()))
                    {
                        foreach (var productDiscount in bm.ProductDiscounts)
                        {
                            productDiscount.ProductId = bm.ProductId;
                            discountManager.Insert(productDiscount);
                        }
                    }

                    using (ProductPriceManager productPriceManager = new ProductPriceManager(new ProductPriceRepository()))
                    {
                        foreach (var productPrice in bm.ProductPrices)
                        {
                            productPrice.ProductId = bm.ProductId;
                            productPriceManager.Insert(productPrice);
                        }
                    }

                    TempData["AddSuccessMessage"] = "Ürün Başarılı bir şekilde eklendi";
                    return RedirectToAction("Products");
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("AddError", "Veri eklenirken veritabanında bir hata meydana geldi.");
                return AddProduct(bm);
            }
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            int ATTIBUTEMAXCOUNT = 1;

            ProductManager pm=new ProductManager(new ProductRepository());
            var productBm = pm.SelectById(id);
            //productBm.ProductAttributes = new List<Entities.Concrete.ProductAttribute>();
            if (productBm.ProductPrices==null)
            {
                productBm.ProductPrices=new List<ProductPrice>();
            }
            if (productBm.ProductAttributes.Count == 0)
            {
                for (int i = 0; i < ATTIBUTEMAXCOUNT; i++)
                {
                    productBm.ProductAttributes.Add(new Entities.Concrete.ProductAttribute());
                }
            }

            TempData["ProductAttributesCompare"] = productBm.ProductAttributes;
            using (SegmentManager m = new SegmentManager())
            {
                var segments = m.GetAll();
                foreach (var segment in segments)
                {

                    productBm.ProductPrices.Add(new Entities.Concrete.ProductPrice() { SegmentId = segment.SegmentId });
                    productBm.ProductDiscounts.Add(new Entities.Concrete.ProductDiscount() { SegmentId = segment.SegmentId });


                }
            }
            return View(productBm);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Entities.Concrete.Product bm)
        {
            using (BLL.Concrete.ProductManager productManager = new BLL.Concrete.ProductManager(new ProductRepository()))
            {
                try
                {
                    productManager.Update(bm);
                    List<Entities.Concrete.ProductAttribute> firstAttributes = TempData["ProductAttributesCompare"] as List<Entities.Concrete.ProductAttribute>;
                    UpdateProductAttributes(bm.ProductAttributes, firstAttributes,bm.ProductId);
                    TempData["AddSuccessMessage"] = "Ürün Başarılı bir şekilde güncellendi";
                    return RedirectToAction("Products");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("AddError", "Veri eklenirken veritabanında bir hata meydana geldi.");
                    return View(bm);

                }
            }
        }

        private bool UpdateProductAttributes(List<Entities.Concrete.ProductAttribute> sonGelenler, List<Entities.Concrete.ProductAttribute> ilkTutulan, int productId)
        {
            /// Son Gelen Listenin İçinde İlk Gelenlerden Yoksa Silinmiştir
            /// Son Gelen Listenin İçinde İlk Gelenlerden Yoksa ve bu ilk gelenlerdede yoksa Eklenmiştir
            /// Diğer durumda güncellenmiştir


            using (ProductAttributeManager m = new ProductAttributeManager(new ProductAttributeRepository()))
            {
                foreach (Entities.Concrete.ProductAttribute productAttribute in ilkTutulan)
                {
                    productAttribute.ProductId = productId;
                    var isDeleted = sonGelenler.FirstOrDefault(x => x.RelationId == productAttribute.RelationId) == null;
                    if (isDeleted)
                    {
                        m.Delete(productAttribute.RelationId);

                    }
                }
                foreach (Entities.Concrete.ProductAttribute attribute in sonGelenler)
                {
                    attribute.ProductId = productId;
                    if (attribute.RelationId != 0)
                    {
                        m.Update(attribute);
                    }
                    else
                    {
                        m.Insert(attribute);
                    }
                }


            }

            return true;
        }

        public ActionResult DeleteProduct(int id)
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
            return View(new UserManager(new UserRepository()).SelectAll());
            
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new User());
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddUser(User bm)
        {
           
            using (UserManager m=new UserManager(new UserRepository()))
            {
               var result= m.Insert(bm);
               TempData["UserSuccessMessage"] = result.Id>0 ?"Success !":"Failed!";

            }
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            return View(new UserManager(new UserRepository()).SelectById(id));
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateUser(User bm)
        {
           
            using (UserManager m = new UserManager(new UserRepository()))
            {
                var result = m.Update(bm);
                TempData["UserSuccessMessage"] = result.Id > 0 ? "Success !" : "Failed!";

            }
            TempData["UserSuccessMessage"] = "Success !";
            return RedirectToAction("Users");
        }

        public ActionResult DeleteUser(int id)
        {
            using (UserManager m = new UserManager(new UserRepository()))
            {
                var result = m.Delete(id);
                TempData["UserSuccessMessage"] = result.Id > 0 ? "Success !" : "Failed!";

            }
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

            var orderMasterBm = new OrderMasterBM(id);
            
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
            var orderMasterBm = new OrderMasterBM(id);
            TempData["OrderDetailsCompare"] = orderMasterBm.OrderDetails;
            if (orderMasterBm.OrderDetails.Count == 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    orderMasterBm.OrderDetails.Add(new OrderDetail());
                }
            }
            return View(orderMasterBm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateOrder(OrderMasterBM bm)
        {
            using (OrderMasterBusinessManager manager = new OrderMasterBusinessManager())
            {
                manager.Update(bm);
                List<OrderDetail> firstAttributes = TempData["OrderDetailsCompare"] as List<OrderDetail>;
                manager.UpdateOrderDetails(bm.OrderDetails, firstAttributes,bm.OrderMaster.OrderId);
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

        public ActionResult Segments()
        {
            using (SegmentBusinessManager manager = new SegmentBusinessManager())
            {
                var result = manager.Get();
                return View(result);
            }
            
        }

        public ActionResult DeleteSegments(int id)
        {
            using (SegmentBusinessManager manager = new SegmentBusinessManager())
            {
                manager.Delete(new SegmentBM(id));
            }
            return RedirectToAction("Segments", "Admin");
        }
        [HttpGet]
        public ActionResult UpdateSegment(int id)
        {
            return View(new SegmentBM(id));
        }
        [HttpPost]
        public ActionResult UpdateSegment(SegmentBM segmentBm)
        {
            using (SegmentBusinessManager manager = new SegmentBusinessManager())
            {
                manager.Update(segmentBm);
            }
            TempData["MessageSegment"] = "Segment Updated!";
            return RedirectToAction("Segments", "Admin");
        }
        [HttpGet]
        public ActionResult AddSegment()
        {
            return View(new SegmentBM(null));
        }
        [HttpPost]
        public ActionResult AddSegment(SegmentBM segmentBm)
        {
            using (SegmentBusinessManager manager = new SegmentBusinessManager())
            {
                manager.Add(segmentBm);
            }
            TempData["MessageSegment"] = "Segment Added!";
            return RedirectToAction("Segments","Admin");
        }
    }
}