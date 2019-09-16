using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.DbModels
{
    public class OptionMaster
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public string OptionType { get; set; }
        public List<OptionDetail> OptionDetails { get; set; }
    }
}