using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OrderReturnBM
    {
        public OrderReturn OrderReturn { get; set; }
        public OrderMasterBM OrderBM { get; set; }
        public ProductBM ProductBm { get; set; }
        public CustomerBM CustomerBm { get; set; }
        public OrderReturnBM(int? id)
        {
            if (id != null)
            {
                using (OrderReturnsManager m = new OrderReturnsManager())
                {
                    OrderReturn = m.Get((int)id);
                    OrderBM=new OrderMasterBM(OrderReturn.OrderId);
                    CustomerBm = OrderBM.CustomerBm;
                }
                return;
            }
            OrderReturn = new OrderReturn();
            OrderBM = new OrderMasterBM(null);
            ProductBm = new ProductBM(null);
        }
    }
}