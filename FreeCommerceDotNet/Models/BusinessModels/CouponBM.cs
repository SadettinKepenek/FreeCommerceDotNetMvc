using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.BusinessModels
{
    public class CouponBM
    {
        public Coupon Coupon { get; set; }
        public CouponBM()
        {
            Coupon = new Coupon();
        }

        public CouponBM(int? id)
        {
            if (id != null)
            {
                int key = (int)id;
                using (CouponManager cm = new CouponManager())
                {
                    this.Coupon = cm.Get(key);
                }
            }
        }
    }
}