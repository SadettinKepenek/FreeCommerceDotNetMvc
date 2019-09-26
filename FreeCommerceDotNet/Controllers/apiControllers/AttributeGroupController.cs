using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FreeCommerceDotNet.Models.BusinessManager;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Controllers.apiControllers
{
    public class AttributeGroupController : ApiController
    {
        // GET: api/AttributeGroup
        public IEnumerable<AttributeGroupBM> Get()
        {
            using (AttributeGroupBusinessManager bm = new AttributeGroupBusinessManager())
            {
                return bm.Get();
            }
        }

        // GET: api/AttributeGroup/5
        public AttributeGroupBM Get(int id)
        {
            using (AttributeGroupBusinessManager bm = new AttributeGroupBusinessManager())
            {
                return bm.GetById(id);
            }
        }

        // POST: api/AttributeGroup
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AttributeGroup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AttributeGroup/5
        public void Delete(int id)
        {
        }
    }
}
