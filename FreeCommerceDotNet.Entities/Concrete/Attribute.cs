using FreeCommerceDotNet.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "AttributeName  boş geçilemez")]
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
        [Required(ErrorMessage = "AttributeGroup boş geçilemez")]
        public AttributeGroup AttributeGroup { get; set; }


    }
}