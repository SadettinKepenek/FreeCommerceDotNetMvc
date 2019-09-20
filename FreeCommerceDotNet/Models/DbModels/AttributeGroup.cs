using System.Collections;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.DbModels
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

        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}