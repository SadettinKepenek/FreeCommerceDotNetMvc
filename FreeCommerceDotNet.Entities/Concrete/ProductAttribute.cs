using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ProductAttribute:IEntity
    {
        public int RelationId { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeDescription { get; set; }

        public Attribute AttributeBm { get; set; }

    }
}