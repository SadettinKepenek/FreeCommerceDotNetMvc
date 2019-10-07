using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class ProductDiscountManager:IProductDiscountService
    {
        private IProductDiscountDal _discountDal;

        public ProductDiscountManager()
        {
            
        }

        public ProductDiscountManager(IProductDiscountDal discountDal)
        {
            _discountDal = discountDal;
        }
        public ServiceResult Insert(ProductDiscount entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_discountDal.Insert(entity));
        }

        public ServiceResult Update(ProductDiscount entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_discountDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_discountDal.Delete(id));
        }

        public ProductDiscount SelectById(int id)
        {
            return _discountDal.SelectById(id);
        }

        public List<ProductDiscount> SelectByFilter(List<DBFilter> filters)
        {
            return _discountDal.SelectByFilter(filters);
        }

        public List<ProductDiscount> SelectAll()
        {
            return _discountDal.SelectAll();
        }

        public void Dispose()
        {
        }
    }
}