using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Customer = FreeCommerceDotNet.Entities.Concrete.Customer;
using CustomerManager = FreeCommerceDotNet.BLL.Concrete.CustomerManager;
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

using AttributeGroupManager = FreeCommerceDotNet.BLL.Concrete.AttributeGroupManager;
using AttributeManager = FreeCommerceDotNet.BLL.Concrete.AttributeManager;
using AttributeGroup = FreeCommerceDotNet.Entities.Concrete.AttributeGroup;
using Attribute = FreeCommerceDotNet.Entities.Concrete.Attribute;
using Segment = FreeCommerceDotNet.Entities.Concrete.Segment;
using SegmentManager = FreeCommerceDotNet.BLL.Concrete.SegmentManager;




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
                var segments = m.SelectAll();
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
                var segments = m.SelectAll();
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
            List<Customer> customers;
            using (CustomerManager manager = new CustomerManager(new CustomerRepositorycs()))
            {
                customers = manager.SelectAll();
            }
            return View(customers);
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer customerModel)
        {
            using (BLL.Concrete.UserManager m=new BLL.Concrete.UserManager(new DAL.Concrete.UserRepository()))
            {
                int userId=m.Insert(new Entities.Concrete.User() {
                    EMail=customerModel.Email,
                    Password=customerModel.Password,
                    Username=customerModel.Email,
                    Role="Client",
                    
                }).Id;
                customerModel.UserId = userId;
            }
            using (CustomerManager manager = new CustomerManager(new CustomerRepositorycs()))
            {

                manager.Insert(customerModel);
            }

            TempData["Message"] = "Customer Added!";
            return RedirectToAction("Customers", "Admin");

        }

        public ActionResult DeleteCustomer(int id)
        {
            using (CustomerManager manager = new CustomerManager(new CustomerRepositorycs()))
            {
                manager.Delete(id);
            }

            return RedirectToAction("Customers", "Admin");

        }

        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            using (CustomerManager manager = new CustomerManager(new CustomerRepositorycs()))
            {
                return View(manager.SelectById(id));
            }
            
        }
        [HttpPost]
        public ActionResult UpdateCustomer(Customer customerModel)
        {
            using (BLL.Concrete.UserManager m = new BLL.Concrete.UserManager(new DAL.Concrete.UserRepository()))
            {
                int userId = m.Update(new Entities.Concrete.User()
                {
                    EMail = customerModel.Email,
                    Password = customerModel.Password,
                    UserId = customerModel.UserId,
                    Username = customerModel.Email, //kanka user id döndürmüyor ki update?
                    Role = "Client",
                    

                }).Id;
            }
            using (CustomerManager manager = new CustomerManager(new CustomerRepositorycs()))
            {
               manager.Update(customerModel);
            }
            TempData["Message"] = "Customer Updated!";
            return RedirectToAction("Customers", "Admin");

        }

        [HttpGet]
        public ActionResult AddAttributeGroup()
        {
            
            return View(new AttributeGroup());
        }
        [HttpPost]
        public ActionResult AddAttributeGroup(AttributeGroup attributeGroupModel)
        {
            using (AttributeGroupManager manager = new AttributeGroupManager(new AttributeGroupRepository()))
            {
                manager.Insert(attributeGroupModel);
            }
            TempData["MessageAttributeGroup"] = "Attribute Group Added!";
            return RedirectToAction("AttributeGroupList", "Admin");
        }

        public ActionResult AttributeGroupList()
        {
            List<AttributeGroup> attributeGroupList;
            using (AttributeGroupManager manager = new AttributeGroupManager(new AttributeGroupRepository()))
            {
                attributeGroupList = manager.SelectAll();
            }
            return View(attributeGroupList);
        }

        public ActionResult DeleteAttributeGroup(int id)
        {
            using (AttributeGroupManager manager = new AttributeGroupManager(new AttributeGroupRepository()))
            {
                manager.Delete(id);
            }
            return RedirectToAction("AttributeGroupList", "Admin");
        }


        public ActionResult AttributeList()
        {
            List<Attribute> allAttributes;
            using (AttributeManager manager = new AttributeManager(new AttributeRepository()))
            {
                allAttributes = manager.SelectAll();
            }
            return View(allAttributes);
        }
        [HttpGet]
        public ActionResult UpdateAttributeGroup(int id)
        {
            using (AttributeGroupManager manager = new AttributeGroupManager(new AttributeGroupRepository()))
            {
                return View(manager.SelectById(id));
            }
        }
        [HttpPost]
        public ActionResult UpdateAttributeGroup(AttributeGroup attributeGroupModel)
        {
            using (AttributeGroupManager manager = new AttributeGroupManager(new AttributeGroupRepository()))
            {
                manager.Update(attributeGroupModel);
            }
            TempData["MessageAttributeGroup"] = "Attribute Group Updated!";
            return RedirectToAction("AttributeGroupList", "Admin");
        }

        [HttpGet]
        public ActionResult UpdateAttribute(int id)
        {
            using (AttributeManager manager = new AttributeManager(new AttributeRepository()))
            {
                return View(manager.SelectById(id)); 
            }
           
        }
        [HttpPost]
        public ActionResult UpdateAttribute(Attribute attributeModel)
        {
            using (AttributeManager manager = new AttributeManager(new AttributeRepository()))
            {
                manager.Update(attributeModel);
            }
            TempData["MessageAttribute"] = "Attribute Updated!";
            return RedirectToAction("AttributeList", "Admin");
        }
        public ActionResult DeleteAttribute(int id)
        {
            using (AttributeManager manager = new AttributeManager(new AttributeRepository()))
            {
                manager.Delete(id);
            }

            return RedirectToAction("AttributeList", "Admin");
        }

        [HttpGet]
        public ActionResult AddAttribute()
        {
            return View(new Attribute());
        }
        [HttpPost]
        public ActionResult AddAttribute(Attribute attributeModel)
        {
            using (AttributeManager manager = new AttributeManager(new AttributeRepository()))
            { 
                manager.Insert(attributeModel);  
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
            
            return View(new OrderMasterManager(new OrderMasterRepository()).SelectAll());
        }

        public ActionResult OrderDetail(int id)
        {

            return View(new OrderMasterManager(new OrderMasterRepository()).SelectById(id));

        }

        [HttpGet]
        public ActionResult AddOrder()
        {

            var orderMasterBm = new OrderMaster();
            orderMasterBm.OrderDetails=new List<Entities.Concrete.OrderDetail>();
            for (int i = 0; i <= 1; i++)
            {
                orderMasterBm.OrderDetails.Add(new Entities.Concrete.OrderDetail());
            }
            return View(orderMasterBm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddOrder(OrderMaster bm)
        {

            using (OrderMasterManager manager = new OrderMasterManager(new OrderMasterRepository()))
            {
                int orderMasterId=manager.Insert(bm).Id;
                bm.OrderId = orderMasterId;
                
            }

            using (OrderDetailManager m=new OrderDetailManager(new OrderDetailRepository()))
            {
                foreach (Entities.Concrete.OrderDetail detail in bm.OrderDetails)
                {
                    detail.OrderId = bm.OrderId;
                    m.Insert(detail);

                }
            }
            TempData["OrderMasterMessage"] = "Order Has Been Added";

            return RedirectToAction("Orders");
        }

        [HttpGet]
        public ActionResult UpdateOrder(int id)
        {
            var orderMasterBm = new OrderMasterManager(new OrderMasterRepository()).SelectById(id);
            TempData["OrderDetailsCompare"] = orderMasterBm.OrderDetails;
            orderMasterBm.OrderDetails = orderMasterBm.OrderDetails ?? new List<Entities.Concrete.OrderDetail>();
            if (orderMasterBm.OrderDetails.Count == 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    orderMasterBm.OrderDetails.Add(new Entities.Concrete.OrderDetail());
                }
            }
            return View(orderMasterBm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateOrder(OrderMaster bm)
        {
            using (OrderMasterManager manager = new OrderMasterManager(new OrderMasterRepository()))
            {
                manager.Update(bm);
                List<Entities.Concrete.OrderDetail> firstAttributes = TempData["OrderDetailsCompare"] as List<Entities.Concrete.OrderDetail>;
                UpdateOrderDetails(bm.OrderDetails, firstAttributes,bm.OrderId);
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

        private void UpdateOrderDetails(List<Entities.Concrete.OrderDetail> sonGelenler, List<Entities.Concrete.OrderDetail> ilkTutulan, int orderMasterId)
        {
            using (OrderDetailManager m = new OrderDetailManager(new OrderDetailRepository()))
            {
                foreach (Entities.Concrete.OrderDetail orderDetail in ilkTutulan)
                {
                    orderDetail.OrderId = orderMasterId;
                    var isDeleted = sonGelenler.FirstOrDefault(x => x.OrderDetailId == orderDetail.OrderDetailId) == null;
                    if (isDeleted)
                    {
                        m.Delete(orderDetail.OrderDetailId);

                    }
                }
                foreach (Entities.Concrete.OrderDetail orderDetail in sonGelenler)
                {
                    orderDetail.OrderId = orderMasterId;
                    if (orderDetail.OrderDetailId != 0)
                    {
                        //Update
                        m.Update(orderDetail);
                    }
                    else
                    {
                        m.Insert(orderDetail);
                    }
                }


            }
        }


        public ActionResult Segments()
        {
            using (SegmentManager manager = new SegmentManager(new SegmentRepository()))
            {       
                return View(manager.SelectAll());
            }
            
        }

        public ActionResult DeleteSegments(int id)
        {
            using (SegmentManager manager = new SegmentManager(new SegmentRepository()))
            {
                manager.Delete(id);
            }
            TempData["MessageSegment"] = "Segment Deleted!";
            return RedirectToAction("Segments", "Admin");
        }
        [HttpGet]
        public ActionResult UpdateSegment(int id)
        {
            using (SegmentManager manager = new SegmentManager(new SegmentRepository()))
            {
                return View(manager.SelectById(id));
            }
            

        }
        [HttpPost]
        public ActionResult UpdateSegment(Segment segmentModel)
        {
            using (SegmentManager manager = new SegmentManager(new SegmentRepository()))
            {
                manager.Update(segmentModel);
            }
            TempData["MessageSegment"] = "Segment Updated!";
            return RedirectToAction("Segments", "Admin");
        }
        [HttpGet]
        public ActionResult AddSegment()
        {
            using (SegmentManager manager = new SegmentManager(new SegmentRepository()))
            {
                return View(new Segment());
            }
        }
        [HttpPost]
        public ActionResult AddSegment(Segment segmentModel)
        {
            using (SegmentManager manager = new SegmentManager(new SegmentRepository()))
            {
                manager.Insert(segmentModel);
            }
            TempData["MessageSegment"] = "Segment Added!";
            return RedirectToAction("Segments","Admin");
        }
    }
}