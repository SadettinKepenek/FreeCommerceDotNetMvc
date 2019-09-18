using System;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.Interfaces
{
    public interface IBusinessOperations<T>:IDisposable
    {
        List<T> GetAll();
        T GetById(int id);
        List<T> GetByKey(int id, string tbl, string key);
        bool Add(T entry);
        bool AddRange(List<T> entryList);
        bool Delete(T entry);
        bool DeleteRange(List<T> entryList);
        bool Update(T entry);

    }
}