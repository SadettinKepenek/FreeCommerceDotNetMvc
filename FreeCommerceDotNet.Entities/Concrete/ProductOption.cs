using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ProductOption:IEntity
    {
        public int RelationId { get; set; }
        public int ProductId { get; set; }
        public int ValueId { get; set; }
        public int Quantity { get; set; }
        public double AdditionalPrice { get; set; }
        public double AdditionalWeight { get; set; }
    }
}