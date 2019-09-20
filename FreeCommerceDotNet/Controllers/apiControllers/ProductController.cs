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
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Controllers.apiControllers
{
    public class ProductController : ApiController
    {
        private ProductManager productManager =new ProductManager();
        // GET: api/Product



        public IEnumerable<AttributeBM> GetAllProducts()
        {
            return null;
        }

        // GET: api/Product/5
        public AttributeBM GetProductById(int id)
        {
            return null;
        }

        // POST: api/Product
        public void PostProduct([FromBody]string product)
        {
            var p = Utilities.FromJson<Product>(product);
            if (p!=null)
            {
                productManager.Add(p);

            }
        }

        // PUT: api/Product/5
        public void Put([FromBody]string value)
        {
            Product p = Utilities.FromJson<Product>(value);
            if (p != null)
            {
                productManager.Update(p);

            }
        }

        // DELETE: api/Product/5
        public HttpStatusCodeResult Delete(int id)
        {
            productManager.Delete(id);
            Debug.WriteLine("Silindi");
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }
    }
}
