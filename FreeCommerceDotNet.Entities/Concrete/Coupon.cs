using FreeCommerceDotNet.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Coupon:IEntity
    {
        public int CouponId { get; set; }

        [Required(ErrorMessage = "CouponStartDate  boş geçilemez")]
        public string CouponStartDate { get; set; }

        [Required(ErrorMessage = "CouponEndDate  boş geçilemez")]
        public string CouponEndDate { get; set; }

        [Required(ErrorMessage = "CouponDiscount  boş geçilemez")]
        public string CouponDiscount { get; set; }
        public int CouponQuantity { get; set; }
        public bool CouponStatus { get; set; }
    }
}