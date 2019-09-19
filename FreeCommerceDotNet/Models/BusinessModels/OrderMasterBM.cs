using System.Collections.Generic;
using System.Linq;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OrderMasterBM
    {
        public OrderMaster OrderMaster { get; set; }
        public List<OrderDetailBM> OrderDetails { get; set; }
        public OrderReturnBM OrderReturn { get; set; }
        public CustomerBM CustomerBm { get; set; }
        public PaymentBM PaymentBm { get; set; }
        public ShippingBM ShippingBm { get; set; }
        public OrderMasterBM(int? id)
        {
            if (id == null)
            {
                OrderMaster=new OrderMaster();
                OrderDetails=new List<OrderDetailBM>();
            }
            else
            {
                int key = (int) id;
                using (OrderMasterManager m = new OrderMasterManager())
                {
                    OrderMaster = m.Get(key);
                    CustomerBm=new CustomerBM(OrderMaster.CustomerId);
                    PaymentBm=new PaymentBM(OrderMaster.PaymentGatewayId);
                    ShippingBm=new ShippingBM(OrderMaster.ShippingId);
                }

                using (OrderReturnsManager m = new OrderReturnsManager())
                {
                    var returnOrder = m.GetByIntegerKey((int) id, "OrdersReturns", "OrderId").FirstOrDefault();
                    if (returnOrder != null) {OrderReturn = new OrderReturnBM(returnOrder.ReturnId);}
                    
                }
                using (OrderDetailManager m = new OrderDetailManager())
                {
                    var dbOrderDetails = m.GetByIntegerKey(key, "OrdersDetail", "OrderId");
                    foreach (var dbOrderDetail in dbOrderDetails)
                    {
                        OrderDetails.Add(new OrderDetailBM(dbOrderDetail.OrderDetailId));
                    }
                }

               
            }
        }
    }
}