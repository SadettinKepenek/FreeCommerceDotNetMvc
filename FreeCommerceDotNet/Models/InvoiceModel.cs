using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.Models
{
    public class InvoiceModel
    {
        public Invoice Invoice { get; set; }
        public OrderMaster OrderMaster { get; set; }
        public Customer Customer { get; set; }

    }
}