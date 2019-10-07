using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class ProductAttribute:IEntity
    {
        public int RelationId { get; set; }
        [Required(ErrorMessage = "ProductId Zorunludur")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "AttributeId Zorunludur")]
        public int AttributeId { get; set; }
        [Required(ErrorMessage = "AttributeDescription Zorunludur")]
        public string AttributeDescription { get; set; }

        public Attribute AttributeBm { get; set; }

    }
}