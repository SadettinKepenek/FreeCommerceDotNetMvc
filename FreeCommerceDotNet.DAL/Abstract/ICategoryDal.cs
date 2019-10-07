using System.Collections.Generic;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public interface ICategoryDal:IRepository<Category>
    {
        List<Category> GetLayoutCategories();
    }
}