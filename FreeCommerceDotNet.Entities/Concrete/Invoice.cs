using FreeCommerceDotNet.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Invoice:IEntity
    {
        public int InvoiceId { get; set; }
        [Required(ErrorMessage = "OrderId zorunludur")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "InvoiceTotalPrice zorunludur")]
        public double InvoiceTotalPrice { get; set; }
        public double InvoiceTotalDiscount { get; set; }
        public bool InvoiceStatus{ get; set; }
        public string TranscationNo{ get; set; }
        public OrderMaster OrderMaster { get; set; }

    }
}
