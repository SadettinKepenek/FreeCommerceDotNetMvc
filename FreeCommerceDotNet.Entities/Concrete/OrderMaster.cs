using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class OrderMaster:IEntity
    {
        public int OrderId { get; set; }
        public int PaymentGatewayId { get; set; }

        [Required(ErrorMessage = "ShippingId boş geçilemez")]
        public int ShippingId { get; set; }

        [Required(ErrorMessage = "CustomerId boş geçilemez")]
        public int CustomerId { get; set; }
        public string TrackNumber { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryComment { get; set; }
        public string DeliveryStatus { get; set; }

        [Required(ErrorMessage = "OrderDetails boş geçilemez")]
        public List<OrderDetail> OrderDetails { get; set; }
        public OrderReturn OrderReturn { get; set; }
        public Customer CustomerBm { get; set; }
        public Payment PaymentBm { get; set; }
        public Shipping ShippingBm { get; set; }


    }
}