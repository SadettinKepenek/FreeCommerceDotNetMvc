using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using FreeCommerceDotNet.Models;
using FreeCommerceDotNet.Models.ControllerModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FreeCommerceDotNet.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Index()
        {
            return RedirectToAction("Login", new object() { });
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginModel());
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login, string returnUrl)
        {
            Debug.WriteLine(returnUrl);
            if (ModelState.IsValid)
            {
                List<DBFilter> filters = new List<DBFilter>();
                filters.Add(new DBFilter() { ParamName = "@username", ParamValue = login.Username });
                filters.Add(new DBFilter() { ParamName = "@password", ParamValue = login.Password });

                using (var manager = new UserManager(new UserRepository()))
                {
                    var selectByFilter = manager.SelectByFilter(filters);
                    if (selectByFilter != null)
                    {
                        Entities.Concrete.User user = selectByFilter.FirstOrDefault();

                        AuthCookie(login, user);
                        if (String.IsNullOrEmpty(returnUrl))
                            returnUrl = "~/Home/Index";


                        return Redirect(returnUrl);


                    }
                    else
                    {
                        ModelState.AddModelError("LoginAttempt", "Username or Password are Invalid.");

                        return View(login);
                    }
                }


            }
            else
                return View(login);

        }

        private static void AuthCookie(LoginModel login, User user)
        {
            var userRoles = user.Role;
            var authTicket = new FormsAuthenticationTicket(
                1, // version
                login.Username, // user name
                DateTime.Now, // created
                DateTime.Now.AddMinutes(20), // expires
                login.rememberMe, // persistent?
                userRoles // can be used to store roles
            );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Security", null);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            using (SegmentManager segmentManager = new SegmentManager(new SegmentRepository()))
            {
                ViewBag.Segments = segmentManager.SelectAll();
            }
            return View(new RegisterModel());
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterModel register)
        {
            var user = new User()
            {
                Username = register.Customer.Email,
                Password = register.User.Password,
                Role = "Client",
                EMail = register.Customer.Email
            };
            using (UserManager userManager = new UserManager(new UserRepository()))
            {

                var userInsertResult = userManager.Insert(user);
                if (userInsertResult.Message.Equals("Success"))
                {
                    using (CustomerManager customerManager = new CustomerManager(new CustomerRepositorycs()))
                    {
                        register.Customer.User = user;
                        register.Customer.UserId = userInsertResult.Id;
                        register.Customer.Password = user.Password;
                        var customerInsertResult = customerManager.Insert(register.Customer);
                        if (customerInsertResult.Message.Equals("Success"))
                        {
                            AuthCookie(new LoginModel()
                            {
                                Username = register.User.Username,
                                Roles = "Client",
                                Password = register.User.Password,
                                EMail = register.User.EMail
                            }, user);
                            TempData["Message"] = "Üyeliğiniz başarıyla açıldı";
                            using (OutlookMailManager mailManager = new OutlookMailManager())
                            {
                                mailManager.Send(register.Customer.Email, "Welcome To FreeCommerceDotNet", EmailHelper.RegisterSuccessMail());
                            }
                            return RedirectToAction("Index", "Account");
                        }
                        // Else
                        userManager.Delete(userInsertResult.Id);
                    }
                    //Else

                    return RedirectToAction("Index", "Account");
                }

                return View("Login", register);
            }

        }
    }
}