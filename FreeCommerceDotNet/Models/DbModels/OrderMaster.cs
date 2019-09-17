namespace FreeCommerceDotNet.Models.DbModels
{
    public class OrderMaster
    {
        public int OrderId { get; set; }
        public int PaymentGatewayId { get; set; }
        public int ShippingId { get; set; }
        public int CustomerId { get; set; }
    }
}