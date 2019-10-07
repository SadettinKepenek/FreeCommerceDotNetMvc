using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class StoreManager : IStoreService
    {
        private IStoreDal _storeRepository;
        public StoreManager()
        {

        }
        public StoreManager(IStoreDal storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public ServiceResult Insert(Store entity)
        {
            if (!String.IsNullOrEmpty(entity.StoreName) && !String.IsNullOrEmpty(entity.StoreOwner) && !String.IsNullOrEmpty(entity.CellPhone))
            {
                var result = _storeRepository.Insert(entity);
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.Success, id: result.Id);
            }
            else
            {
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.ParameterError);
            }
        }
        public ServiceResult Update(Store entity)
        {
            if (!String.IsNullOrEmpty(entity.StoreName) && !String.IsNullOrEmpty(entity.StoreOwner) &&  !String.IsNullOrEmpty(entity.CellPhone) && entity.StoreId != 0)
            {
                var result = _storeRepository.Update(entity);
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.Success, id: result.Id);
            }
            else
            {
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.ParameterError);
            }
        }
        public ServiceResult Delete(int id)
        {
            if (id != 0)
            {
                var result = _storeRepository.Delete(id);
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.Success, id: result.Id);
            }
            else
            {
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.ParameterError);
            }
        }
        
        public List<Store> SelectAll()
        {
            return _storeRepository.SelectAll();
        }
        public Store SelectById(int id)
        {
            if (id != 0)
            {

                return _storeRepository.SelectById(id);
            }

            return null;
        }
        public List<Store> SelectByFilter(List<DBFilter> filters)
        {
            return _storeRepository.SelectByFilter(filters);
        }

        

        
    }
}
