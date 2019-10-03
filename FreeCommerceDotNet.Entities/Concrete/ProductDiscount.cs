using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ProductDiscount:IEntity
    {
        public int DiscountId { get; set; }
        public int ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Quantity { get; set; }
        public double NewPrice { get; set; }
        public int SegmentId { get; set; }

        public Product Product;
        public Segment SegmentEntity { get; set; }
    }
}