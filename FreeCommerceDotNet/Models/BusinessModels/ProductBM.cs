using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductBM
    {
        public Product Product { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public List<ProductDiscount> ProductDiscounts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductOption> ProductOptions { get; set; }
        public List<ProductPrice> ProductPrices { get; set; }
        public List<Reviews> Reviews { get; set; }
        public int Id { get; set; }

        public ProductBM(int? id)
        {
            if (id == null)
            {
                this.Product=new Product();
                this.ProductAttributes = new List<ProductAttribute>();
                this.ProductDiscounts = new List<ProductDiscount>();
                this.ProductImages = new List<ProductImage>();
                this.ProductOptions = new List<ProductOption>();
                this.ProductPrices = new List<ProductPrice>();
            }
            else
            {

                int key = (int)id;
                this.Id = key;
                InitializeProduct(key);
            }

        }

        private void InitializeProduct(int key)
        {
            using (var m = new ProductManager())
            {
                this.Product = m.Get(key);
            }

            using (var m = new ProductAttributeManager())
            {
                this.ProductAttributes = m.GetByIntegerKey(key, "ProductsAttributes", "ProductId");
            }

            using (var m = new ProductDiscountManager())
            {
                this.ProductDiscounts = m.GetByIntegerKey(key, "ProductsDiscounts", "ProductId");
            }

            using (var m = new ProductImageManager())
            {
                this.ProductImages = m.GetByIntegerKey(key, "ProductsImages", "ProductId");
            }

            using (var m = new ProductOptionsManager())
            {
                this.ProductOptions = m.GetByIntegerKey(key, "ProductsOptions", "ProductId");
            }

            using (var m = new ProductPriceManager())
            {
                this.ProductPrices = m.GetByIntegerKey(key, "ProductsPrices", "ProductId");
            }

            using (var m = new ReviewsManager())
            {
                this.Reviews = m.GetByIntegerKey(key, "Reviews", "ProductId");
            }
        }
    }
}