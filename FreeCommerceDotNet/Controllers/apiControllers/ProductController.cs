using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FreeCommerceDotNet.Models;

namespace FreeCommerceDotNet.Controllers.apiControllers
{
    public class ProductController : ApiController
    {
        private DbManager dbManager=new DbManager();
        // GET: api/Product
        public IEnumerable<Product> Get()
        {
            return dbManager.GetProducts(null);
        }

        // GET: api/Product/5
        public List<Product> Get(int id)
        {
            return dbManager.GetProducts(id);
        }

        // POST: api/Product
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
        }
    }
}
