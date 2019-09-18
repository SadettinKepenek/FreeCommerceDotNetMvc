using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public List<ProductDiscount> ProductDiscounts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductOption> ProductOptions { get; set; }
        public List<ProductPrice> ProductPrices { get; set; }


        public ProductModel(int productId)
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

        public ProductModel()
        {
            
        }

        
    }
}