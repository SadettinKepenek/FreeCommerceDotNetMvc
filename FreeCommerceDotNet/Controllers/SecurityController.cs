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

                        var userRoles = user.Role;
                        var authTicket = new FormsAuthenticationTicket(
                            1,                             // version
                            login.Username,                      // user name
                            DateTime.Now,                  // created
                            DateTime.Now.AddMinutes(20),   // expires
                            login.rememberMe,                    // persistent?
                            userRoles                     // can be used to store roles
                        );

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
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
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Security", null);
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {

            return View(new ForgetPasswordModel());
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            var varifyUrl = "";
            using (UserManager userManager = new UserManager(new UserRepository()))
            {
                var user = userManager.SelectByFilter(new List<DBFilter>(){
                    new DBFilter()
                    {
                        ParamName = "@email",
                        ParamValue = forgetPasswordModel.Email
                    }
                }
                 ).FirstOrDefault();
                var ticketid = userManager.InsertResetTicket(user.UserId).Id;
                var token = userManager.SelectResetTicket(ticketid).tokenHash;

                var url = "http://" + Request.Url.Host +":"+Request.Url.Port+ "/Home/ChangePassword?ticketid="+ticketid+"&token="+token;
                varifyUrl = "<a href = '" + url + "'>'" + url + "'</a>";
            }
            using (OutlookMailManager mailManager = new OutlookMailManager())
            {
                mailManager.Send("sadettin.kepenek@hotmail.com", "Change Password", "Şifrenizi yeniden oluşturmak için linke bağlantıya tıklayın:'" + varifyUrl + "'");
                TempData["MailMessage"] = "Lütfen e posta adresinizi kontrol edin";
            }
            return View();
        }

    }
}