using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class OrderMaster:IEntity
    {
        public int OrderId { get; set; }
        public int PaymentGatewayId { get; set; }
        public int ShippingId { get; set; }
        public int CustomerId { get; set; }
        public string TrackNumber { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryComment { get; set; }
        public string DeliveryStatus { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public OrderReturn OrderReturn { get; set; }
        public Customer CustomerBm { get; set; }
        public Payment PaymentBm { get; set; }
        public Shipping ShippingBm { get; set; }


    }
}