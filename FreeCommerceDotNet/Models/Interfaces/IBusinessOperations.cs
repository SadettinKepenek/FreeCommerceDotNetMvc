using System;
using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.Interfaces
{
    public interface IBusinessOperations<T>:IDisposable
    {
        List<T> Get();
        T GetById(int id);
        int Add(T entry);
        bool Update(T entry);
        bool Delete(T entry);

    }
}