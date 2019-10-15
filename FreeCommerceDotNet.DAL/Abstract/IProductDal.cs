using FreeCommerceDotNet.Entities.Concrete;
using System.Collections.Generic;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public interface IProductDal:IRepository<Product>
    {
        List<Product> SearchProduct(string productName);   
    }
}