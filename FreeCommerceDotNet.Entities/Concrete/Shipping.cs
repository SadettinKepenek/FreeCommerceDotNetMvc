using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Shipping:IEntity
    {
        public int ShippingId { get; set; }
        public string ShippingName { get; set; }
        public string ShippingDescription { get; set; }
        public List<OrderMaster> OrderMasterBms { get; set; }

    }
}