using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductDal _productDal;

        public ProductManager()
        {
            
        }

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public ServiceResult Insert(Product entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_productDal.Insert(entity));
        }

        public ServiceResult Update(Product entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_productDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_productDal.Delete(id));
        }

        public Product SelectById(int id)
        {
            return _productDal.SelectById(id);
        }

        public List<Product> SelectByFilter(List<DBFilter> filters)
        {
            return _productDal.SelectByFilter(filters);
        }

        public List<Product> SelectAll()
        {
            return _productDal.SelectAll();
        }

        public void Dispose()
        {
        }
    }
}