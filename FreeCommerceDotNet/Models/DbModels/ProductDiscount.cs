namespace FreeCommerceDotNet.Models.DbModels
{
    public class ProductDiscount
    {
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Quantity { get; set; }
        public double NewPrice { get; set; }
        public string Segment { get; set; }
    }
}