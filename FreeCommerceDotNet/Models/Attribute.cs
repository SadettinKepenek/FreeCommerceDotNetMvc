namespace FreeCommerceDotNet.Models
{
    public class Attribute
    {

        private int _AttributeId;

        public int AttributeId
        {
            get { return _AttributeId; }
            set { _AttributeId = value; }
        }
        private string _AttributeName;

        public string AttributeName
        {
            get { return _AttributeName; }
            set { _AttributeName = value; }
        }

        private int _AttributeGroupId;

        public int AttributeGroupId
        {
            get { return _AttributeGroupId; }
            set { _AttributeGroupId = value; }
        }
        private AttributeGroup _attributeGroup;

        public AttributeGroup AttributeGroup
        {
            get { return _attributeGroup; }
            set { _attributeGroup = value; }
        }



    }
}