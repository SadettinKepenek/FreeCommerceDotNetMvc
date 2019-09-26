﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FreeCommerceDotNet.Models;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Index()
        {
            return RedirectToAction("Login",new object(){} );
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
        public ActionResult Login(LoginModel login,string returnUrl)
        {
            Debug.WriteLine(returnUrl);
            if (ModelState.IsValid)
            {
                
                string sqlQuery = "select * from Users where Username=@username and Password=@password";
                SqlCommand cmd=new SqlCommand(sqlQuery);
                cmd.Parameters.AddWithValue("@username", login.Username);
                cmd.Parameters.AddWithValue("@password", login.Password);

                using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
                {
                    cmd.Connection = connection;
                    if(connection.State==ConnectionState.Closed)
                        connection.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            var roles = reader[4].ToString();
                            var authTicket = new FormsAuthenticationTicket(
                                1,                             // version
                                login.Username,                      // user name
                                DateTime.Now,                  // created
                                DateTime.Now.AddMinutes(20),   // expires
                                login.rememberMe,                    // persistent?
                                roles                     // can be used to store roles
                            );

                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
                            connection.Close();
                        }

                        if (String.IsNullOrEmpty(returnUrl))
                            returnUrl = "~/Home/Index";
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("LoginAttempt","Username or Password are Invalid.");
                        connection.Close();
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
    }
}