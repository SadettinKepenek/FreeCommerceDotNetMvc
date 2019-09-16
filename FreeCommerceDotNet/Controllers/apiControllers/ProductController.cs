using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FreeCommerceDotNet.Models;

namespace FreeCommerceDotNet.Controllers.apiControllers
{
    public class ProductController : ApiController
    {
        private DbManager dbManager=new DbManager();
        // GET: api/Product
        public IEnumerable<Product> GetAllProducts()
        {   
            return dbManager.GetProducts(null);
        }

        // GET: api/Product/5
        public List<Product> GetProductById(int id)
        {
            string p = Utilities.ToJson(dbManager.GetProducts(id).FirstOrDefault());
            PostProduct(p);
            return dbManager.GetProducts(id);
        }

        // POST: api/Product
        public void PostProduct([FromBody]string product)
        {
            var p = Utilities.FromJson<Product>(product);
            if (p!=null)
            {
                dbManager.AddProduct(p);

            }
        }

        // PUT: api/Product/5
        public void Put([FromBody]string value)
        {
            Product p = Utilities.FromJson<Product>(value);
            if (p != null)
            {
                dbManager.UpdateProducts(p);

            }
        }

        // DELETE: api/Product/5
        public HttpStatusCodeResult Delete(int id)
        {
            dbManager.deleteProduct(id);
            Debug.WriteLine("Silindi");
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
    }
}
