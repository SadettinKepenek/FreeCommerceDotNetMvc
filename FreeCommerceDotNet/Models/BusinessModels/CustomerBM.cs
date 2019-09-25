using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class CustomerBM
    {
        public Customer Customer { get; set; }
        public Segment Segment { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<OrderMaster> OrderMasters { get; set; }
        public List<OrderReturn> OrderReturns { get; set; }

        public CustomerBM() { }
        public CustomerBM(int? id)
        {
            if (id == null)
            {
                Customer = new Customer();
                Segment = new Segment();
                Reviews = new List<Reviews>();
                OrderMasters = new List<OrderMaster>();
                OrderReturns = new List<OrderReturn>();
            }
            else
            {
                int key = (int)id;
                using (CustomerManager m = new CustomerManager())
                {
                    Customer = m.Get(key);
                }

                using (SegmentManager m = new SegmentManager())
                {
                    Segment = m.Get(Customer.SegmentId);
                }

                using (ReviewsManager m = new ReviewsManager())
                {
                    Reviews = m.GetByIntegerKey(key, "Reviews", "CustomerId");
                    
                }

                using (OrderMasterManager m = new OrderMasterManager())
                {
                    OrderMasters= m.GetByIntegerKey(key, "OrdersMaster", "CustomerId");
                    
                }

                using (OrderReturnsManager m = new OrderReturnsManager())
                {
                    OrderReturns = m.GetByIntegerKey(key, "OrdersReturns", "CustomerId");
                    
                }
            }
        }
    }
}