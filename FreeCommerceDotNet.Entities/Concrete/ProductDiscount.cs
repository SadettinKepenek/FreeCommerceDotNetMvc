using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ProductDiscount:IEntity
    {
        public int DiscountId { get; set; }
        [Required(ErrorMessage = "ProductId Zorunludur")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "StartDate Zorunludur")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "EndDate Zorunludur")]
        public string EndDate { get; set; }
        [Required(ErrorMessage = "Quantity Zorunludur")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "NewPrice Zorunludur")]
        public double NewPrice { get; set; }
        [Required(ErrorMessage = "SegmentId Zorunludur")]
        public int SegmentId { get; set; }

        public Product Product;
        public Segment SegmentEntity { get; set; }
    }
}