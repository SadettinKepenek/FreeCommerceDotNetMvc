using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Shipping:IEntity
    {
        public int ShippingId { get; set; }
        [Required(ErrorMessage = "Shipping Adı Zorunludur")]
        public string ShippingName { get; set; }
        [Required(ErrorMessage = "Shipping Description Zorunludur")]
        public string ShippingDescription { get; set; }
        public List<OrderMaster> OrderMasterBms { get; set; }

    }
}