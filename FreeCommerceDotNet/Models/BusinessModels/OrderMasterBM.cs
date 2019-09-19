using System.Collections.Generic;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OrderMasterBM
    {
        public OrderMaster OrderMaster { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<OrderReturn> OrderReturns { get; set; }

        public OrderMasterBM(int? id)
        {
            if (id == null)
            {
                OrderMaster=new OrderMaster();
                OrderDetails=new List<OrderDetail>();
                OrderReturns=new List<OrderReturn>();
            }
            else
            {
                int key = (int) id;
                using (OrderMasterManager m = new OrderMasterManager())
                {
                    OrderMaster = m.Get(key);
                }

                using (OrderDetailManager m = new OrderDetailManager())
                {
                    OrderDetails = m.GetByIntegerKey(key, "OrdersDetail", "OrderId");
                }

                using (OrderReturnsManager m = new OrderReturnsManager())
                {
                    OrderReturns= m.GetByIntegerKey(key, "OrdersReturns", "OrderId");
                }
            }
        }
    }
}