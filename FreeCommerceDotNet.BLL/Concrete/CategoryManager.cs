using System.Collections.Generic;
using FreeCommerceDotNet.BLL.Abstract;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager()
        {
            
        }

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public ServiceResult Insert(Category entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_categoryDal.Insert(entity));
        }

        public ServiceResult Update(Category entity)
        {
            return ServiceHelper.FromDbResultToServiceResult(_categoryDal.Update(entity));
        }

        public ServiceResult Delete(int id)
        {
            return ServiceHelper.FromDbResultToServiceResult(_categoryDal.Delete(id));
        }

        public Category SelectById(int id)
        {
            return _categoryDal.SelectById(id);
        }

        public List<Category> SelectByFilter(List<DBFilter> filters)
        {
            return _categoryDal.SelectByFilter(filters);
        }

        public List<Category> SelectAll()
        {
            return _categoryDal.SelectAll();
        }
    }
}