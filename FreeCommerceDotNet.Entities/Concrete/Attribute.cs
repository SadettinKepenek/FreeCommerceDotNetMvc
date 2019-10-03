using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Attribute:IEntity
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

        private int _AttributeGroup;

        public int AttributeGroupId
        {
            get { return _AttributeGroup; }
            set { _AttributeGroup = value; }
        }

        public AttributeGroup AttributeGroup { get; set; }


    }
}