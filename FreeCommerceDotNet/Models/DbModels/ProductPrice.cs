namespace FreeCommerceDotNet.Models.DbModels
{
    public class ProductPrice
    {
        public int PriceId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Segment { get; set; }
    }
}