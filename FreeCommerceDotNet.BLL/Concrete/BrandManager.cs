using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandRepository;
        
        public BrandManager()
        {

        }
        public BrandManager(IBrandDal brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public ServiceResult Insert(Brand entity)
        {
            if (!String.IsNullOrEmpty(entity.BrandName) && !String.IsNullOrEmpty(entity.BrandDescription))
            {
                var result = _brandRepository.Insert(entity);
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.Success,id:result.Id);
            }
            else
            {
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.ParameterError);
            }

        }

        public ServiceResult Update(Brand entity)
        {
            if (!String.IsNullOrEmpty(entity.BrandName) && !String.IsNullOrEmpty(entity.BrandDescription) && entity.BrandId!=0)
            {
                var result = _brandRepository.Update(entity);
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
                var result = _brandRepository.Delete(id);
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.Success, id: result.Id);
            }
            else
            {
                return ServiceHelper.CreateServiceResultMessage(ResultMessageType: ServiceReturn.ParameterError);
            }
        }

        public Brand SelectById(int id)
        {
            if (id != 0)
            {

                return _brandRepository.SelectById(id);
            }

            return null;
        }

        public List<Brand> SelectByFilter(List<DBFilter> filters)
        {
            return _brandRepository.SelectByFilter(filters);
        }

        public List<Brand> SelectAll()
        {
            return _brandRepository.SelectAll();

        }
    }
}