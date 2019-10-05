﻿namespace FreeCommerceDotNet.Entities.Concrete
{
    public class OrderReturn
    {
        public int ReturnId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int BoxOpened { get; set; }
        public bool ReturnStatus { get; set; }
        public string ReturnReason { get; set; }
        public string Comment { get; set; }
        public string ReturnResponse { get; set; }

        public OrderMaster OrderBM { get; set; }
        public Product ProductBm { get; set; }
        public Customer CustomerBm { get; set; }

    }
}