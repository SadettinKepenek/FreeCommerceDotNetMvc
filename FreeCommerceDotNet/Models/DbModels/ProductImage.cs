namespace FreeCommerceDotNet.Models.DbModels
{
    public class ProductImage
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}