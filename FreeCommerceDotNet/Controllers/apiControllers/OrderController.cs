using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.WebApi;
using FreeCommerceDotNet.Models.WebApi.Helper;

namespace FreeCommerceDotNet.Controllers.apiControllers
{
    public class OrderController : ApiController
    {
        // GET: api/Order
        [WebApiAuthorize(Roles = "Admin")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [WebApiAuthorize]
        public string Get(int id)
        {
            var order = new OrderMasterBM(id);
            if (order.CustomerBm!=null)
            {
                bool isAuth = WebAPIHelper.isAuthorized(Request.GetOwinContext(), order.CustomerBm.UserId);
                if (isAuth)
                {
                    // Continue
                }
              
            }

            return "value";
        }

        // POST: api/Order
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Order/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Order/5
        public void Delete(int id)
        {
        }
    }
}
