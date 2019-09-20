using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class ProductImageBusinessManager:IBusinessOperations<ProductImageBM>
    {
        public void Dispose()
        {

        }

        public List<ProductImageBM> Get()
        {
            using (ProductImageManager m = new ProductImageManager())
            {
                List<ProductImage> dbProducts = m.GetAll();
                List<ProductImageBM> businessModels = new List<ProductImageBM>();
                foreach (var product in dbProducts)
                {
                    businessModels.Add(new ProductImageBM(product.ProductId));
                }

                return businessModels;
            }
        }

        public ProductImageBM GetById(int id)
        {
            using (ProductImageManager m = new ProductImageManager())
            {
                return new ProductImageBM(id);
            }
        }

        public int Add(ProductImageBM entry)
        {
            using (ProductImageManager m = new ProductImageManager())
            {
                return m.Add(entry.ProductImage);
            }
        }

        public bool Update(ProductImageBM entry)
        {
            using (ProductImageManager m = new ProductImageManager())
            {
                m.Update(entry.ProductImage);
                return true;
            }
        }

        public bool Delete(ProductImageBM entry)
        {
            using (ProductImageManager m = new ProductImageManager())
            {
                m.Delete(entry.ProductImage.ProductId);
                return true;
            }
        }
    }
}