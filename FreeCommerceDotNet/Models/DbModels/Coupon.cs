﻿namespace FreeCommerceDotNet.Models.DbModels
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string CouponStartDate { get; set; }
        public string CouponEndDate { get; set; }
        public string CouponDiscount { get; set; }
        public int CouponQuantity { get; set; }
        public bool CouponStatus { get; set; }

    }
}