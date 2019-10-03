using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class OptionDetail:IEntity
    {
        public int ValueId { get; set; }
        public string ValueName { get; set; }
        public int OptionId { get; set; }
        public OptionMaster OptionMaster { get; set; }

    }
}