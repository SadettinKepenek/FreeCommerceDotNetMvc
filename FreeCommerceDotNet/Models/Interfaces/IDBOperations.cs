using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.Interfaces
{
    public interface IDBOperations<T>
    {
        List<T> GetAll();
        T Get(int id);
        bool Add(T entry);
        int Update(T entry);
        bool Delete(int id);
        bool CheckIsExist(int id);
        List<T> GetByIntegerKey(int id, string tbl, string key);
    }
}