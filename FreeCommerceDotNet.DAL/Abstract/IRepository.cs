using System.Collections.Generic;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public interface IRepository<TEntity>
    {
        DBResult Insert(TEntity entity);
        DBResult Update(TEntity entity);
        DBResult Delete(int id);
        TEntity SelectById(int id);
        List<TEntity> SelectByFilter(List<DBFilter> filters);
        List<TEntity> SelectAll();
    }
}