using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.Interfaces;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ProductBusinessManager : IBusinessOperations<ProductBM>
    {
        public void Dispose()
        {
            
        }

        public List<ProductBM> Get()
        {
            using (ProductManager m = new ProductManager())
            {
                List<Product> dbProducts = m.GetAll();
                List<ProductBM> businessModels = new List<ProductBM>();
                foreach (var product in dbProducts)
                {
                    businessModels.Add(new ProductBM(product.ProductId));
                }

                return businessModels;
            }
        }

        public ProductBM GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Add(ProductBM entry)
        {
            
            int InsertedID = new ProductManager().Add(entry.Product);
            entry.Id = InsertedID;

            throw new System.NotImplementedException();
        }

        public bool Update(ProductBM entry)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(ProductBM entry)
        {
            throw new System.NotImplementedException();
        }
    }
}