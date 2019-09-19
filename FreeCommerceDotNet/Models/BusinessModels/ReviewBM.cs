using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class ReviewBM
    {
        public Reviews Reviews { get; set; }
        public Product Products { get; set; }
        public Customer Customers { get; set; }
        public ReviewBM(int? id)
        {
            if (id == null)
            {
                Reviews = new Reviews();
            }
            else
            {
                using (var m = new ReviewsManager())
                {
                    int key = (int)id;
                    Reviews = m.Get(key);
                }
                using (ProductManager m = new ProductManager())
                {
                    Products = m.Get(Reviews.ProductId);
                }
                using (CustomerManager m = new CustomerManager())
                {
                    Customers = m.Get(Reviews.CustomerId);
                }
            }
        }
    }
}