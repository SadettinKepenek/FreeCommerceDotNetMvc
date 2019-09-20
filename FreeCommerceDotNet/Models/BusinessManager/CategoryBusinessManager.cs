using System;
using System.Collections.Generic;
using FreeCommerceDotNet.Models.BusinessModels;
using FreeCommerceDotNet.Models.DbManager;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;

namespace FreeCommerceDotNet.Models.BusinessManager
{
    public class CategoryBusinessManager : IBusinessOperations<CategoryBM>
    {
        public int Add(CategoryBM entry)
        {
            using (CategoryManager manager = new CategoryManager())
            {
                return manager.Add(entry.Category);
            }
        }

        public bool Delete(CategoryBM entry)
        {
            using (CategoryManager manager = new CategoryManager())
            {
                return manager.Delete(entry.Category.CaregoryId);
            }
        }

        public void Dispose()
        {
            
        }

        public List<CategoryBM> Get()
        {
            using (CategoryManager manager = new CategoryManager())
            {
                List<Category> dbCategories = manager.GetAll();
                List<CategoryBM> businessModels = new List<CategoryBM>();

                foreach (var categories in dbCategories)
                {
                    businessModels.Add(new CategoryBM(categories.CaregoryId));
                }

                return businessModels;
            }
        }

        public CategoryBM GetById(int id)
        {
            return new CategoryBM(id);
        }

        public bool Update(CategoryBM entry)
        {
            try
            {
                using (CategoryManager manager = new CategoryManager())
                {
                    manager.Update(entry.Category);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}