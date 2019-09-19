﻿using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ProductPriceBM
    {
        public ProductPrice ProductPrice { get; set; }
        public List<Product> Products { get; set; }

        public ProductPriceBM(int? id)
        {
            if (id == null)
            {
                ProductPrice = new ProductPrice();
                Products = new List<Product>();

            }
            else
            {

                using (var m = new ProductPriceManager())
                {
                    int key = (int)id;
                    ProductPrice = m.Get(key);
                }

                using (var m = new ProductManager())
                {
                    int key = (int)id;
                    Products = m.GetByIntegerKey(key, "Products", "ProductId");
                }
            }
        }

    }
}