using FreeCommerceDotNet.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class OrderDetail:IEntity
    {
        public int OrderDetailId { get; set; }

        [Required(ErrorMessage = "ProductId boş geçilemez")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "OrderId boş geçilemez")]
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public double ProductPrice { get; set; }
        public bool isDiscountedPrice { get; set; }

        [Required(ErrorMessage = "OrderMaster boş geçilemez")]
        public OrderMaster OrderMaster { get; set; }
        public Product ProductBm { get; set; }

    }
}