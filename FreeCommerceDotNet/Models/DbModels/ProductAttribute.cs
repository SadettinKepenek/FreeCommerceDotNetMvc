namespace FreeCommerceDotNet.Models.DbModels
{
    public class ProductAttribute
    {
        public int RelationId { get; set; }
        public int ProductId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeDescription { get; set; }
    }
}