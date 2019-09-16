using System.Collections.Generic;

namespace FreeCommerceDotNet.Models
{
    public class AttributeGroup
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
        private List<Attribute> _attributes;

        private List<Attribute> Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

    }
}