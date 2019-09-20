using System;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using System.Collections.Generic;

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
            using (ProductManager m = new ProductManager())
            {
                return new ProductBM(id);
            }
        }

        public int Add(ProductBM entry)
        {
            using (ProductManager m = new ProductManager())
            {
                int insertedId = m.Add(entry.Product);
                entry.Id = insertedId;
                using (ProductAttributeManager attributeManager = new ProductAttributeManager())
                {
                    foreach (var entryProductAttribute in entry.ProductAttributes)
                    {
                        entryProductAttribute.ProductId = entry.Id;
                        attributeManager.Add(entryProductAttribute);
                    }
                }

                using (ProductDiscountManager discountManager = new ProductDiscountManager())
                {
                    foreach (var productDiscount in entry.ProductDiscounts)
                    {
                        productDiscount.ProductId = entry.Id;
                        discountManager.Add(productDiscount);
                    }
                }

                using (ProductImageManager imageManager = new ProductImageManager())
                {
                    foreach (var productImage in entry.ProductImages)
                    {
                        productImage.ProductId = entry.Id;
                        imageManager.Add(productImage);
                    }
                }

                using (ProductOptionsManager optionsManager = new ProductOptionsManager())
                {
                    foreach (var productOption in entry.ProductOptions)
                    {
                        productOption.ProductId = entry.Id;
                        optionsManager.Add(productOption);
                    }
                }
                using (ProductPriceManager productPriceManager = new ProductPriceManager())
                {
                    foreach (var productPrice in entry.ProductPrices)
                    {
                        productPrice.ProductId = entry.Id;
                        productPriceManager.Add(productPrice);
                    }
                }

                return entry.Id;
            }

        }

        public bool Update(ProductBM entry)
        {
            try
            {
                using (ProductManager m = new ProductManager())
                {
                    int insertedId = m.Update(entry.Product);
                    using (ProductAttributeManager attributeManager = new ProductAttributeManager())
                    {
                        foreach (var entryProductAttribute in entry.ProductAttributes)
                        {
                            attributeManager.Update(entryProductAttribute);
                        }
                    }

                    using (ProductDiscountManager discountManager = new ProductDiscountManager())
                    {
                        foreach (var productDiscount in entry.ProductDiscounts)
                        {
                            discountManager.Update(productDiscount);
                        }
                    }

                    using (ProductImageManager imageManager = new ProductImageManager())
                    {
                        foreach (var productImage in entry.ProductImages)
                        {
                            imageManager.Update(productImage);
                        }
                    }

                    using (ProductOptionsManager optionsManager = new ProductOptionsManager())
                    {
                        foreach (var productOption in entry.ProductOptions)
                        {
                            optionsManager.Update(productOption);
                        }
                    }
                    using (ProductPriceManager productPriceManager = new ProductPriceManager())
                    {
                        foreach (var productPrice in entry.ProductPrices)
                        {
                            productPriceManager.Update(productPrice);
                        }
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ProductBM entry)
        {
            throw new System.NotImplementedException();
        }
    }
}