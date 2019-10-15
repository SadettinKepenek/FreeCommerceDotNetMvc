namespace FreeCommerceDotNet.Models
{
    public class CartModel
    {
        public int productId { get; set; }
        public int productCount { get; set; }
        public string productImageUrl { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }
    }
}