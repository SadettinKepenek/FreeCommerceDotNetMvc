using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using FreeCommerceDotNet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace FreeCommerceDotNet.Controllers
{
    [Authorize(Roles = "Client")]
    public class AccountController : Controller
    {
        private Customer customer;
        // GET: Account
        public ActionResult Index()
        {
            Customer customer = GetCustomerByContextName();
            ViewBag.CustomerInstance = customer;

            return View();
        }

        private Customer GetCustomerByContextName()
        {
            Customer customer;
            using (CustomerManager customerManager = new CustomerManager(new CustomerRepositorycs()))
            {
                var username = HttpContext.User.Identity.Name;
                customer = customerManager.SelectByFilter(new List<DBFilter>()
                {
                    new DBFilter()
                    {
                        ParamName = "@username",
                        ParamValue = username
                    }
                }).FirstOrDefault();
            }

            return customer;
        }

        public ActionResult ChangePassword()
        {

            return View(GetCustomerByContextName());
        }

        [WebMethod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(int customerId, string newPassword)
        {
            using (UserManager m = new UserManager(new UserRepository()))
            {
                var user = m.SelectByFilter(new List<DBFilter>()
                {
                    new DBFilter()
                    {
                        ParamName = "@customerid",
                        ParamValue = customerId
                    }
                }).FirstOrDefault();
                if (user != null)
                {
                    user.Password = newPassword;
                    var result = m.Update(user);
                    return Json(result.Message);
                }
            }
            return Json("Failed");
        }
        [HttpGet]
        public ActionResult Settings()
        {

            return View(GetCustomerByContextName());
        }

        [HttpPost]
        public ActionResult UpdateProfile(Customer c)
        {
            try
            {
                using (CustomerManager m = new CustomerManager(new CustomerRepositorycs()))
                {
                    var result = m.Update(c);
                    if (result.Message.Equals("Success"))
                    {
                        TempData["Message"] = "Settings has been updated";
                    }
                    else
                    {
                        TempData["Message"] = "Some Error Occured " + result.Message;
                    }

                }
            }
            catch (Exception e)
            {
                TempData["Message"] = "Some Error Occured " + e.StackTrace;

            }
            return RedirectToAction("Settings", "Account");
        }

        public ActionResult MyCart()
        {

            return View();
        }

        public ActionResult MyWishList()
        {
            var customer = GetCustomerByContextName();

            List<Wish> wishLists;
            using (WishlistManager wishlist = new WishlistManager(new WishlistRepository()))
            {
                wishLists = wishlist.SelectByFilter(new List<DBFilter>() {new DBFilter()
                {
                    ParamName = "@CustomerId",
                    ParamValue = customer.CustomerId
                }});
            }
            if (wishLists == null)
            {
                return View(new List<Wish>());
            }
            return View(wishLists);
        }
        public ActionResult DeleteWish(int wishId)
        {
            using (WishlistManager wishlist = new WishlistManager(new WishlistRepository()))
            {
                wishlist.Delete(wishId);
            }
            return RedirectToAction("MyWishList", "Account");
        }
        public ActionResult AddWishList(int productId, int customerId)
        {
            using (WishlistManager manager = new WishlistManager(new WishlistRepository()))
            {
                Wish wish = new Wish();
                wish.ProductId = productId;
                wish.CustomerId = customerId;
                wish.WishDate = DateTime.Now.ToString("dd/MM/yyyy");
                manager.Insert(wish);
            }
            return RedirectToAction("MyWishList", "Account");
        }
        public ActionResult MyOrders()
        {
            if (this.customer == null)
            {
                customer = GetCustomerByContextName();
            }
            using (OrderMasterManager m = new OrderMasterManager(new OrderMasterRepository()))
            {
                var orders = m.SelectByFilter(new List<DBFilter>()
                {
                    new DBFilter()
                    {
                        ParamName = "@CustomerId",
                        ParamValue = customer.CustomerId
                    }
                });
                orders = orders ?? new List<OrderMaster>();
                return View(orders);
            }
        }

        public ActionResult OrderDetail(int id)
        {
            using (OrderMasterManager m = new OrderMasterManager(new OrderMasterRepository()))
            {
                var selectById = m.SelectById(id);
                if (GetCustomerByContextName().CustomerId==selectById.CustomerId)
                {
                    return View(selectById);

                }

                return RedirectToAction("MyOrders", "Account");
            }
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            HttpCookie cartListCookie = Request.Cookies.Get("cartListCookie");
            CheckoutModel model = new CheckoutModel();

            if (cartListCookie != null)
            {
                var cartList = JsonConvert.DeserializeObject<List<CartModel>>(cartListCookie.Value);
                AssignShippingAndPaymentMethods();

                model.Customer = GetCustomerByContextName();
                model.CartList = cartList;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Account");

            }

        }

        private void AssignShippingAndPaymentMethods()
        {
            using (ShippingManager m = new ShippingManager(new ShippingRepository()))
            {
                ViewBag.ShippingMethods = m.SelectAll();
            }

            using (PaymentGatewayManager m = new PaymentGatewayManager(new PaymentGatewayRepository()))
            {
                ViewBag.PaymentMethods = m.SelectAll();
            }
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutModel model)
        {
            // Todo Checkout Logic

            OrderMaster orderMasterTemp = null;
            Invoice invoiceTemp = null;

            if (model.ShippingId ==0 || model.PaymentId==0)
            {
                AssignShippingAndPaymentMethods();
                TempData["Message"] = "Lütfen Ödeme yöntemi ve kargolama yöntemini seçin";
                return View(model);
            }

            try
            {
                var checkoutCustomer = GetCustomerByContextName();
                if (checkoutCustomer != null)
                {

                    using (OrderMasterManager m = new OrderMasterManager(new OrderMasterRepository()))
                    {
                        string expectedDeliveryDate = m.GetExpectedDeliveryDate();

                        OrderMaster master = new OrderMaster();
                        master.CustomerId = checkoutCustomer.CustomerId;
                        master.CustomerBm = checkoutCustomer;
                        master.DeliveryDate = expectedDeliveryDate;
                        master.DeliveryComment = String.Empty;
                        master.DeliveryStatus = "Hazırlanıyor";
                        master.OrderDate = DateTime.Now.ToShortDateString();
                        master.PaymentGatewayId = model.PaymentId;
                        master.ShippingId = model.ShippingId;
                        master.TrackNumber = String.Empty;
                        master.OrderDetails = new List<OrderDetail>();

                        var filters = new List<DBFilter>();
                        filters.Add(new DBFilter() { ParamName = "@segmentId", ParamValue = checkoutCustomer.SegmentId });
                        using (ProductPriceManager priceManager = new ProductPriceManager(new ProductPriceRepository()))
                        {
                            foreach (CartModel cartModel in model.CartList)
                            {
                                var price = priceManager.SelectByFilter(filters).FirstOrDefault();
                                var orderDetail = new OrderDetail()
                                {
                                    ProductId = cartModel.productId,
                                    ProductPrice = price.Price,
                                    Quantity = cartModel.productCount,
                                };

                                master.OrderDetails.Add(orderDetail);
                            }
                        }

                        var result = m.Insert(master);
                        master.OrderId = result.Id;
                        if (result.Message.Equals("Success"))
                        {
                            // Order Master Başarıyla Eklendiyse
                            using (OrderDetailManager detailManager = new OrderDetailManager(new OrderDetailRepository()))
                            {
                                orderMasterTemp = master;

                                foreach (var masterOrderDetail in master.OrderDetails)
                                {
                                    masterOrderDetail.OrderId = master.OrderId;
                                    var r = detailManager.Insert(masterOrderDetail);
                                    masterOrderDetail.OrderDetailId = r.Id;
                                }

                                if (master.OrderDetails.All(x => x.ProductId != 0))
                                {
                                    // Order Detaillar başarı ile eklendi ise



                                    using (InvoiceManager invoiceManager = new InvoiceManager(new InvoiceRepository()))
                                    {
                                        var invoice = new Invoice()
                                        {
                                            OrderMaster = master,
                                            OrderId = master.OrderId,
                                            InvoiceStatus = true,
                                            InvoiceTotalDiscount = 0.000,
                                            InvoiceTotalPrice = master.OrderDetails.Sum(x => x.ProductPrice),
                                            TranscationNo = invoiceManager.GetTranscationNumber(),

                                        };
                                        var invoiceResult = invoiceManager.Insert(invoice);
                                        if (invoiceResult.Message.Equals("Success"))
                                        {
                                            invoice.InvoiceId = invoiceResult.Id;
                                            invoiceTemp = invoice;

                                            // Invoice başarı ile eklendi ise
                                            var httpCookie = Request.Cookies.Get("cartListCookie");
                                            if (httpCookie != null)
                                            {
                                                httpCookie.Expires = DateTime.Now.AddDays(-1);
                                                Response.Cookies.Add(httpCookie);
                                            }

                                            new OutlookMailManager().Send(checkoutCustomer.User.EMail,
                                                $"Order-{invoice.TranscationNo}", "<h3>Your Order Is Preparing</h3><hr/>" +
                                                                                                   "<div class=\"container\">" +
                                                                                                   "<div class=\"row\"><p>Dear " + checkoutCustomer.Firstname + " " + checkoutCustomer.Lastname + " Your Order is preparing" +
                                                                                                   "<br/> Your Expected Delivery Date is " + master.DeliveryDate + " Have Good Day.<p></div>" +
                                                                                                   "</div>");

                                            return RedirectToAction("OrderSuccess", "Account", new { orderId = result.Id });
                                        }
                                        else
                                        {
                                            return CancelOrder(model, master, detailManager, m, result);
                                        }
                                    }
                                }
                                else
                                {
                                    return CancelOrder(model, master, detailManager, m, result);
                                }
                            }


                        }
                        else
                        {
                            AssignShippingAndPaymentMethods();
                            TempData["Message"] = result.Message;
                            return View(model);
                        }

                    }
                }
                TempData["Message"] = "Something happened";

                AssignShippingAndPaymentMethods();
                return View(model);
            }
            catch (Exception e)
            {
                return CancelOrder(model, orderMasterTemp, new OrderDetailManager(new OrderDetailRepository()), new OrderMasterManager(new OrderMasterRepository()),invoiceTemp,e);

                
            }

        }

        private ActionResult CancelOrder(CheckoutModel model, OrderMaster orderMasterTemp, OrderDetailManager detailManager, OrderMasterManager orderMasterManager, Invoice invoiceTemp,Exception e)
        {
            AssignShippingAndPaymentMethods();

            if (orderMasterTemp.OrderId != 0)
            {
                orderMasterManager.Delete(orderMasterTemp.OrderId);
            }

            if (orderMasterTemp.OrderDetails != null)
                foreach (OrderDetail detail in orderMasterTemp.OrderDetails.Where(x => x.OrderDetailId != 0))
                {
                    detailManager.Delete(detail.OrderDetailId);
                }

            if (invoiceTemp.InvoiceId!=0)
            {
                using (InvoiceManager invoiceManager=new InvoiceManager(new InvoiceRepository()))
                {
                    invoiceManager.Delete(invoiceTemp.InvoiceId);
                }
            }
            orderMasterManager.Delete(orderMasterTemp.OrderId);
            TempData["Message"] = e.StackTrace;
            return View(model);
        }

        private ActionResult CancelOrder(CheckoutModel model, OrderMaster master, OrderDetailManager detailManager,
            OrderMasterManager m, ServiceResult result)
        {

            AssignShippingAndPaymentMethods();

            if (master.OrderId != 0)
            {
                m.Delete(master.OrderId);
            }

            if (master.OrderDetails != null)
                foreach (OrderDetail detail in master.OrderDetails.Where(x => x.OrderDetailId != 0))
                {
                    detailManager.Delete(detail.OrderDetailId);
                }

           
            m.Delete(result.Id);
            return View(model);
        }
        

        [HttpGet]
        public ActionResult OrderSuccess(int orderId)
        {
            using (OrderMasterManager m = new OrderMasterManager(new OrderMasterRepository()))
            {
                return View(m.SelectById(orderId));
            }
        }


        [HttpGet]
        public ActionResult Invoice(int orderId)
        {
            var customer = GetCustomerByContextName();

            InvoiceModel invoiceModel =new InvoiceModel();
            using (OrderMasterManager m=new OrderMasterManager(new OrderMasterRepository()))
            {
                invoiceModel.OrderMaster = m.SelectById(orderId);
            }

            if (invoiceModel.OrderMaster.CustomerId!=customer.CustomerId)
            {
                return RedirectToAction("Index", "Account");
            }

            using (InvoiceManager m=new InvoiceManager(new InvoiceRepository()))
            {
                var filters = new List<DBFilter>();
                filters.Add(new DBFilter(){ParamValue = orderId,ParamName = "@OrderId" });
                invoiceModel.Invoice = m.SelectByFilter(filters).FirstOrDefault();
            }

            invoiceModel.Customer = customer;

            return View(invoiceModel);
        }

    }
}