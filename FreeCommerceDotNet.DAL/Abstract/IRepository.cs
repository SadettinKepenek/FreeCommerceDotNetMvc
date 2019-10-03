using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public interface IRepository<TEntity>
    {
        DBResult Insert(TEntity entity);
        DBResult Update(TEntity entity);
        DBResult Delete(int id);
        TEntity SelectById(int id);
        List<TEntity> SelectAll();
    }
}