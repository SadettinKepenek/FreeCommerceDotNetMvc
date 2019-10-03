using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class OptionMaster:IEntity
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public string OptionType { get; set; }
        public List<OptionDetail> OptionDetails { get; set; }

    }
}