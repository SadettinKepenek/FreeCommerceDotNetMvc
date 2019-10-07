using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class ProductPriceManager:IProductPriceService
    {
        private IProductPriceDal _priceDal;

        public ProductPriceManager()
        {
            
        }

        public ProductPriceManager(IProductPriceDal priceDal)
        {
            _priceDal = priceDal;
        }
        public ServiceResult Insert(ProductPrice entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_priceDal.Insert(entity));
        }

        public ServiceResult Update(ProductPrice entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_priceDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_priceDal.Delete(id));
        }

        public ProductPrice SelectById(int id)
        {
            return _priceDal.SelectById(id);
        }

        public List<ProductPrice> SelectByFilter(List<DBFilter> filters)
        {
            return _priceDal.SelectByFilter(filters);
        }

        public List<ProductPrice> SelectAll()
        {
            return _priceDal.SelectAll();
        }

        public void Dispose()
        {
        }
    }
}