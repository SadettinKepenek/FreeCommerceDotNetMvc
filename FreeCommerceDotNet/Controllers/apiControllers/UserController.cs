using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;

namespace FreeCommerceDotNet.Controllers.apiControllers
{
    [Authorize]
    public class UserController : ApiController
    {
        // GET: api/User
        public List<UsersBM> Get()
        {
            using (UsersBusinessManager bm = new UsersBusinessManager())
            {
                return bm.Get();
            }
        }

        // GET: api/User/5
        public UsersBM Get(int id)
        {
            using (UsersBusinessManager bm=new UsersBusinessManager())
            {
                return bm.GetById(id);
            }
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
