using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class BusinessProductModel
    {
        public DBProduct Product { get; set; }
        public List<DBProductAttribute> ProductAttributes { get; set; }
        public List<DBProductDiscount> ProductDiscounts { get; set; }
        public List<DBProductImage> ProductImages { get; set; }
        public List<DBProductOption> ProductOptions { get; set; }
        public List<DBProductPrice> ProductPrices { get; set; }


        public BusinessProductModel(int productId)
        {
            using (ProductManager manager = new ProductManager())
            {
                this.Product = manager.Get(productId);
            }
            using (ProductAttributeManager manager = new ProductAttributeManager())
            {
                this.ProductAttributes =
                    manager.GetByIntegerKey(this.Product.ProductId, "ProductsAttributes", "ProductId");
            }
            using (ProductDiscountManager manager = new ProductDiscountManager())
            {
                this.ProductDiscounts =
                    manager.GetByIntegerKey(this.Product.ProductId, "ProductsDiscounts", "ProductId");
            }
            using (ProductImageManager manager = new ProductImageManager())
            {
                this.ProductImages =
                    manager.GetByIntegerKey(this.Product.ProductId, "ProductsImages", "ProductId");
            }
            using (ProductOptionsManager manager = new ProductOptionsManager())
            {
                this.ProductOptions =
                    manager.GetByIntegerKey(this.Product.ProductId, "ProductsOptions", "ProductId");
            }
            using (ProductPriceManager manager = new ProductPriceManager())
            {
                this.ProductPrices =
                    manager.GetByIntegerKey(this.Product.ProductId, "ProductsPrices", "ProductId");
            }
        }

        public BusinessProductModel()
        {
            
        }

        
    }
}