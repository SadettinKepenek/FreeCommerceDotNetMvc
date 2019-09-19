using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductBM
    {
        public Product Product { get; set; }
        public List<ProductAttributeBM> ProductAttributes { get; set; }
        public List<ProductDiscountBM> ProductDiscounts { get; set; }
        public List<ProductImageBM> ProductImages { get; set; }
        public List<ProductOptionBM> ProductOptions { get; set; }
        public List<ProductPriceBM> ProductPrices { get; set; }
        public List<ReviewBM> Reviews { get; set; }
        public CategoryBM Category { get; set; }
        public int Id { get; set; }

        public ProductBM(int? id)
        {
            if (id == null)
            {
                this.Product=new Product();
                this.ProductAttributes = new List<ProductAttributeBM>();
                this.ProductDiscounts = new List<ProductDiscountBM>();
                this.ProductImages = new List<ProductImageBM>();
                this.ProductOptions = new List<ProductOptionBM>();
                this.ProductPrices = new List<ProductPriceBM>();
                this.Category = new CategoryBM(null);

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
                this.Category =new CategoryBM(Product.CategoryId);
            }

            using (var m = new ProductAttributeManager())
            {
                var dbAttributes = m.GetByIntegerKey(key, "ProductsAttributes", "ProductId");
                foreach (var productAttribute in dbAttributes)
                {
                    ProductAttributes.Add(new ProductAttributeBM(productAttribute.RelationId));
                }
            }

            using (var m = new ProductDiscountManager())
            {
                var dbDiscounts = m.GetByIntegerKey(key, "ProductsDiscounts", "ProductId");
                foreach (var productDiscount in dbDiscounts)
                {
                    ProductDiscounts.Add(new ProductDiscountBM(productDiscount.DiscountId));
                }
            }

            using (var m = new ProductImageManager())
            {
                var dbImages = m.GetByIntegerKey(key, "ProductsImages", "ProductId");
                foreach (var productImage in dbImages)
                {
                    ProductImages.Add(new ProductImageBM(productImage.ImageId));
                }
            }

            using (var m = new ProductOptionsManager())
            {
                var dbOptions = m.GetByIntegerKey(key, "ProductsOptions", "ProductId");
                foreach (var dbOption in dbOptions)
                {
                    ProductOptions.Add(new ProductOptionBM(dbOption.RelationId));
                }
            }

            using (var m = new ProductPriceManager())
            {
                var dbPrices = m.GetByIntegerKey(key, "ProductsPrices", "ProductId");
                foreach (var productPrice in dbPrices)
                {
                    ProductPrices.Add(new ProductPriceBM(productPrice.PriceId));
                }
            }

            using (var m = new ReviewsManager())
            {
                var dbReviews=m.GetByIntegerKey(key, "Reviews", "ProductId");
                foreach (var reviewse in dbReviews)
                {
                    Reviews.Add(new ReviewBM(reviewse.ReviewId));
                }
            }

           
        }
    }
}