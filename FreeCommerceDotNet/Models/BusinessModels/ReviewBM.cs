using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ReviewBM
    {
        public Reviews Reviews { get; set; }
        public List<Product> Products { get; set; }

        public List<Customer> Customers { get; set; }
        public ReviewBM(int? id)
        {
            if (id == null)
            {
                Reviews = new Reviews();
                Products = new List<Product>();
                Customers = new List<Customer>();
            }
            else
            {

                using (var m = new ReviewsManager())
                {
                    int key = (int)id;
                    Reviews = m.Get(key);
                }

                using (var m = new ProductManager())
                {
                    int key = (int)id;
                    Products = m.GetByIntegerKey(key, "Products", "ProductId");
                }

                using (var m = new CustomerManager())
                {
                    int key = (int)id;
                    Customers = m.GetByIntegerKey(key, "Customers", "CustomerId");

                }
            }
        }
    }
}