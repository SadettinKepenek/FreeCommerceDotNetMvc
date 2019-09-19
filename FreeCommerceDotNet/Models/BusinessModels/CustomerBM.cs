using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class CustomerBM
    {
        public Customer Customer { get; set; }
        public List<ReviewBM> Reviews { get; set; }
        public List<OrderMasterBM> OrderMasters { get; set; }
        public List<OrderReturnBM> OrderReturns { get; set; }

        public CustomerBM(int? id)
        {
            if (id == null)
            {
                Customer = new Customer();
                Reviews = new List<ReviewBM>();
                OrderMasters = new List<OrderMasterBM>();
                OrderReturns = new List<OrderReturnBM>();
            }
            else
            {
                int key = (int)id;
                using (CustomerManager m = new CustomerManager())
                {
                    Customer = m.Get(key);
                }

                using (ReviewsManager m = new ReviewsManager())
                {
                    var dbReviews = m.GetByIntegerKey(key, "Reviews", "CustomerId");
                    foreach (var reviewse in dbReviews)
                    {
                        Reviews.Add(new ReviewBM(reviewse.ReviewId));
                    }
                }

                using (OrderMasterManager m = new OrderMasterManager())
                {
                    var dbOrderMasters = m.GetByIntegerKey(key, "OrdersMaster", "CustomerId");
                    foreach (OrderMaster master in dbOrderMasters)
                    {
                        OrderMasters.Add(new OrderMasterBM(master.OrderId));
                    }
                }

                using (OrderReturnsManager m = new OrderReturnsManager())
                {
                    var dbOrderReturns = m.GetByIntegerKey(key, "OrdersReturns", "CustomerId");
                    foreach (OrderReturn master in dbOrderReturns)
                    {
                        OrderReturns.Add(new OrderReturnBM(master.OrderId));
                    }
                }
            }
        }
    }
}