using System.Collections.Generic;
using System.Linq;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OrderMasterBM
    {
        public OrderMaster OrderMaster { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public OrderReturn OrderReturn { get; set; }
        public Customer CustomerBm { get; set; }
        public Payment PaymentBm { get; set; }
        public Shipping ShippingBm { get; set; }
        public OrderMasterBM(int? id)
        {
            if (id == null)
            {
                OrderMaster=new OrderMaster();
                OrderDetails=new List<OrderDetail>();
            }
            else
            {
                int key = (int) id;
                using (OrderMasterManager m = new OrderMasterManager())
                {
                    OrderMaster = m.Get(key);
                }

                using (OrderReturnsManager m = new OrderReturnsManager())
                {
                    OrderReturn = m.GetByIntegerKey((int) id, "OrdersReturns", "OrderId").FirstOrDefault();
                    
                }
                using (OrderDetailManager m = new OrderDetailManager())
                {
                   OrderDetails= m.GetByIntegerKey(key, "OrdersDetail", "OrderId");
                   
                }

                using (var m = new CustomerManager())
                {
                    CustomerBm = m.Get(OrderMaster.CustomerId);
                }

                using (var m = new PaymentsManager())
                {
                    PaymentBm = m.Get(OrderMaster.PaymentGatewayId);
                }

                using (var m = new ShippingManager())
                {
                    ShippingBm = m.Get(OrderMaster.ShippingId);

                }
                OrderMaster.CustomerId = CustomerBm.CustomerId;
                OrderMaster.PaymentGatewayId = PaymentBm.PaymentId;
                OrderMaster.ShippingId = ShippingBm.ShippingId;

            }
        }
    }
}