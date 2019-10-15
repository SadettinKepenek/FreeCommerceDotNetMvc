using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
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

        [HttpPost]
        public ActionResult Register(LoginModel register,string returnUrl)
        {
            var user = new User()
            {
                Username = register.Username,
                Password = register.Password,
                Role = "Client",
                EMail = register.EMail
            };
            using (UserManager m=new UserManager(new UserRepository()))
            {
                var result = m.Insert(user);
                if (result.Message.Equals("Success"))
                {
                    register.rememberMe = true;
                    AuthCookie(register,user);
                    TempData["Message"] = "Üyeliğiniz başarıyla açıldı";
                    using ()
                    {
                        
                    }
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    return View("Login",register);
                }
            }

        }
    }
}