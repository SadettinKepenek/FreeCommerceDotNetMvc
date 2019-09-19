using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.Interfaces;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ProductBusinessManager : IBusinessOperations<AttributeBM>
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public List<AttributeBM> Get()
        {
            using (AttributeManager m = new AttributeManager())
            {
                List<Attribute> dbProducts = m.GetAll();
                List<AttributeBM> businessModels = new List<AttributeBM>();
                foreach (var product in dbProducts)
                {
                    businessModels.Add(new AttributeBM(product.AttributeId));
                }

                return businessModels;
            }
        }

        public AttributeBM GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Add(AttributeBM entry)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(AttributeBM entry)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(AttributeBM entry)
        {
            throw new System.NotImplementedException();
        }
    }
}