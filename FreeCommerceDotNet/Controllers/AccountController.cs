using FreeCommerceDotNet.BLL.Concrete;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Concrete;
using FreeCommerceDotNet.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FreeCommerceDotNet.Controllers
{
    [Authorize]
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

        public ActionResult Addresses()
        {
            Customer customer = GetCustomerByContextName();

            return View();
        }
    }
}