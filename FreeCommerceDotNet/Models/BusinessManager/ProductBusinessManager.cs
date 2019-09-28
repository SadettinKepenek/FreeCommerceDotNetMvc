using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FreeCommerceDotNet.Models.Util;

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
            return new ProductBM(id);
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

        public bool UpdateProductAttributes(List<ProductAttribute> sonGelenler, List<ProductAttribute> ilkTutulan,int productId)
        {
            /// Son Gelen Listenin İçinde İlk Gelenlerden Yoksa Silinmiştir
            /// Son Gelen Listenin İçinde İlk Gelenlerden Yoksa ve bu ilk gelenlerdede yoksa Eklenmiştir
            /// Diğer durumda güncellenmiştir
            
            // Silinenleri Bulalım

            List<ProductAttribute> silinenler=new List<ProductAttribute>();
            List<ProductAttribute> eklenenler=new List<ProductAttribute>();
            List<ProductAttribute> guncellenenler=new List<ProductAttribute>();


            using (ProductAttributeManager m = new ProductAttributeManager())
            {
                foreach (ProductAttribute productAttribute in ilkTutulan)
                {
                    productAttribute.ProductId = productId;
                    var isDeleted = sonGelenler.FirstOrDefault(x => x.RelationId == productAttribute.RelationId) == null;
                    if (isDeleted)
                    {
                        m.Delete(productAttribute.RelationId);

                    }
                }
                foreach (ProductAttribute attribute in sonGelenler)
                {
                    attribute.ProductId = productId;
                    if (attribute.RelationId != 0)
                    {
                        //Update
                        m.Update(attribute);
                    }
                    else
                    {
                        m.Add(attribute);
                    }
                }

               
            }
           



           
            return true;
        }

        public bool Update(ProductBM entry)
        {
            try
            {
                using (ProductManager m = new ProductManager())
                {
                    int insertedId = m.Update(entry.Product);
                    //using (ProductAttributeManager attributeManager = new ProductAttributeManager())
                    //{
                    //    foreach (var entryProductAttribute in entry.ProductAttributes)
                    //    {
                    //        attributeManager.Update(entryProductAttribute);
                    //    }
                    //}

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

            using (ProductManager m = new ProductManager())
            {

                /// Kontrol
                /// Sipariş Varmı
                /// Yorum Var Mı
                /// Fatura Var Mı
                return m.Delete(entry.Product.ProductId);
            }


        }

        public DeleteResponseModel DeleteEntry(int id)
        {
            return Utilities.isRemovable("Products",id);
        }

    }
}