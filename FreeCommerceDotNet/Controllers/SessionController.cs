using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeCommerceDotNet.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult SetVariable(string key, string value)
        {
            Session[key] = value;
            return this.Json(new { success = true });
        }

        public ActionResult GetVariable(string key)
        {
            return this.Json(new {key=Session[key]});
        }
        public ActionResult RemoveVariable(string key)
        {
            Session.Remove(key);
            return this.Json(new { success = "Başarı ile silindi" });
        }
    }
}