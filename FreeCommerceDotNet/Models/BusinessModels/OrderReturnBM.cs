using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OrderReturnBM
    {
        public OrderReturn OrderReturn { get; set; }
        public OrderMaster OrderBM { get; set; }
        public Product ProductBm { get; set; }
        public Customer CustomerBm { get; set; }
        public OrderReturnBM(int? id)
        {
            if (id != null)
            {
                using (OrderReturnsManager m = new OrderReturnsManager())
                {
                    OrderReturn = m.Get((int)id);
                }
                using (var m = new OrderMasterManager())
                {
                    OrderBM = m.Get(OrderReturn.OrderId);
                }

                using (var m = new CustomerManager())
                {
                    CustomerBm = m.Get(OrderBM.CustomerId);
                }
                using (var m = new ProductManager())
                {
                    ProductBm = m.Get(OrderReturn.ProductId);
                }

                return;
            }
            OrderReturn = new OrderReturn();

        }
    }
}