using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class AttributeGroup:IEntity
    {
        private int _AttributeGroupId;

        public int AttributeGroupId
        {
            get { return _AttributeGroupId; }
            set { _AttributeGroupId = value; }
        }
        private string _AttributeGroupName;

        public string AttributeGroupName
        {
            get { return _AttributeGroupName; }
            set { _AttributeGroupName = value; }
        }
        public List<Attribute> Attributes { get; set; }

    }
}