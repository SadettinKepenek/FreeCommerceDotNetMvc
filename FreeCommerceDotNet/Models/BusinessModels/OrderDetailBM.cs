using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class OrderDetailBM
    {
        public OrderDetail Detail { get; set; }
        public OrderMaster OrderMaster { get; set; }
        public Product ProductBm { get; set; }

        public OrderDetailBM(int? id)
        {
            if (id != null)
            {
                using (OrderDetailManager m=new OrderDetailManager())
                {
                    Detail = m.Get((int) id);
                }

                using (OrderMasterManager m = new OrderMasterManager())
                {
                    OrderMaster =m.Get(Detail.OrderId);
                }

                using (ProductManager m = new ProductManager())
                {
                    ProductBm = m.Get(Detail.ProductId);

                }

                return;
            }
            Detail=new OrderDetail();
            OrderMaster=new OrderMaster();
            ProductBm=new Product();
        }

    }
}