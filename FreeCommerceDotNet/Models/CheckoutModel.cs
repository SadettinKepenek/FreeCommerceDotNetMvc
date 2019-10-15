using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.Models
{
    public class CheckoutModel
    {
        public List<CartModel> CartList { get; set; }
        public Customer Customer { get; set; }
        public int ShippingId { get; set; }
        public int PaymentId { get; set; }
    }
}