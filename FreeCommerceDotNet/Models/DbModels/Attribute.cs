namespace FreeCommerceDotNet.Models.DbModels
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

        private int _AttributeGroup;

        public int AttributeGroup
        {
            get { return _AttributeGroup; }
            set { _AttributeGroup = value; }
        }
        

    }
}