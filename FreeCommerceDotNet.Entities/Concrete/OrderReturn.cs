using System.ComponentModel.DataAnnotations;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class OrderReturn
    {
        public int ReturnId { get; set; }

        [Required(ErrorMessage = "Order Id Boş Geçilemez")]
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Product Id Boş Geçilemez")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "BoxOpened Boş Geçilemez")]
        public bool BoxOpened { get; set; }
        public bool ReturnStatus { get; set; }
        public string ReturnReason { get; set; }
        public string Comment { get; set; }
        public string ReturnResponse { get; set; }

        public OrderMaster OrderBM { get; set; }
        public Product ProductBm { get; set; }
        public Customer CustomerBm { get; set; }

    }
}