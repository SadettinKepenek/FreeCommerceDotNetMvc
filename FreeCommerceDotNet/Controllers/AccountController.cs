using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
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

            using (WishlistManager wishlist = new WishlistManager(new WishlistRepository()))
            {
                //var list = wishlist.SelectByFilter(new List<DBFilter>() {new DBFilter()
                //{
                //    ParamName = "@CustomerId",
                //    ParamValue = customer.CustomerId
                //}});
            }
            return View();
        }

        public ActionResult MyOrders()
        {
            if (this.customer==null)
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
    }
}