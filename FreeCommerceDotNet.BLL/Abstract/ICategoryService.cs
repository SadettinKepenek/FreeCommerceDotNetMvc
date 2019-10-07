using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Abstract
{
    public interface ICategoryService:IService<Category>
    {
        List<Category> GetLayoutCategories();
    }
}