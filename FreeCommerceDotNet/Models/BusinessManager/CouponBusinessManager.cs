using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class CouponBusinessManager:IBusinessOperations<CouponBM>
    {
        public void Dispose()
        {
        }

        public List<CouponBM> Get()
        {
            using (CouponManager manager = new CouponManager())
            {
                List<Coupon> dbCoupons = manager.GetAll();
                List<CouponBM> businessModel = new List<CouponBM>();

                foreach (var coupon in dbCoupons)
                {
                    businessModel.Add(new CouponBM(coupon.CouponId));
                }
                return businessModel;
            }
        }

        public CouponBM GetById(int id)
        {
           return new CouponBM(id);
        }

        public int Add(CouponBM entry)
        {
            using (CouponManager manager = new CouponManager())
            {
                int insertedId = manager.Add(entry.Coupon);
                entry.Coupon.CouponId= insertedId;

                //using (AttributeManager manager2 = new AttributeManager())
                //{
                //    foreach (var attributes in entry.Attributes)
                //    {
                //        attributes.AttributeId = entry.AttributeGroup.AttributeGroupId;
                //        manager2.Add(attributes);
                //    }
                //}

                return entry.Coupon.CouponId;
            }
        }

        public bool Update(CouponBM entry)
        {
            try
            {
                using (CouponManager manager = new CouponManager())
                {
                    int updatedId = manager.Update(entry.Coupon);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(CouponBM entry)
        {
            using (CouponManager manager = new CouponManager())
            {
                return manager.Delete(entry.Coupon.CouponId);
            }
        }
    }
}