using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ProductPrice:IEntity
    {
        public int PriceId { get; set; }
        [Required(ErrorMessage = "ProductId Zorunludur")]

        public int ProductId { get; set; }
        [Required(ErrorMessage = "Price Zorunludur")]

        public double Price { get; set; }
        [Required(ErrorMessage = "Segment Zorunludur")]

        public int SegmentId { get; set; }

        public Product Product { get; set; }
        public Segment Segment { get; set; }
    }
}