using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ReviewBM
    {
        public Reviews Reviews { get; set; }
        public ProductBM Products { get; set; }
        public CustomerBM Customers { get; set; }
        public ReviewBM(int? id)
        {
            if (id == null)
            {
                Reviews = new Reviews();
                Products = new ProductBM(null);
                Customers = new CustomerBM(null);
            }
            else
            {

                using (var m = new ReviewsManager())
                {
                    int key = (int)id;
                    Reviews = m.Get(key);
                    Products=new ProductBM(Reviews.ProductId);
                    Customers=new CustomerBM(Reviews.CustomerId);
                }

            }
        }
    }
}