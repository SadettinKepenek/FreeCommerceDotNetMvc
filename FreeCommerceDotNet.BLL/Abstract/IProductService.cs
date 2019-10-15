using FreeCommerceDotNet.Entities.Concrete;
using System.Collections.Generic;

namespace FreeCommerceDotNet.BLL.Abstract
{
    public interface IProductService:IService<Product>
    {
        List<Product> SearchProduct(string productName);
    }
}