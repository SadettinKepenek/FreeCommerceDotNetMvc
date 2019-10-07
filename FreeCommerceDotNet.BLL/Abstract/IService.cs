using System.Collections.Generic;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.BLL.Abstract
{
    public interface IService<T>
    {
        ServiceResult Insert(T entity);
        ServiceResult Update(T entity);
        ServiceResult Delete(int id);
        T SelectById(int id);
        List<T> SelectByFilter(List<DBFilter> filters);
        List<T> SelectAll();
    }
}