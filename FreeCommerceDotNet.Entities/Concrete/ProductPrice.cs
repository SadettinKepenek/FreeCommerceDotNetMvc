using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ProductPrice:IEntity
    {
        public int PriceId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int SegmentId { get; set; }

        public Product Product { get; set; }
        public Segment Segment { get; set; }
    }
}