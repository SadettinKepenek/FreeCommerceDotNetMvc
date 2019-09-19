using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OrderDetailBM
    {
        public OrderDetail Detail { get; set; }
        public OrderMasterBM OrderMaster { get; set; }
        public ProductBM ProductBm { get; set; }

        public OrderDetailBM(int? id)
        {
            if (id != null)
            {
                using (OrderDetailManager m=new OrderDetailManager())
                {
                    Detail = m.Get((int) id);
                    OrderMaster = new OrderMasterBM(Detail.OrderId);
                    ProductBm=new ProductBM(Detail.ProductId);
                }

                return;
            }
            Detail=new OrderDetail();
            OrderMaster=new OrderMasterBM(null);
            ProductBm=new ProductBM(null);
        }

    }
}