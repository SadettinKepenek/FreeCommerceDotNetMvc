using System.Collections.Generic;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public interface IRepository<TEntity>
    {
        int Insert(TEntity entity);
        int Update(TEntity entity);
        bool Delete(int id);
        TEntity SelectById(int id);
        List<TEntity> SelectAll();
    }
}