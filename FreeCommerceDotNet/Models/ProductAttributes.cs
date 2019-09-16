namespace FreeCommerceDotNet.Models
{
    
    public class ProductAttributes
    {
        private int _RelationId;

        public int RelationId
        {
            get { return _RelationId; }
            set { _RelationId = value; }
        }
        private int _ProductId;

        public int ProductId
        {
            get { return _ProductId; }
            set { _ProductId = value; }
        }

        private int _AttributeId;

        public int AttributeId
        {
            get { return _AttributeId; }
            set { _AttributeId = value; }
        }
        private Attribute _attribute;

        public Attribute Attribute
        {
            get { return _attribute; }
            set { _attribute = value; }
        }



    }
}