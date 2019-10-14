using System.Collections.Generic;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.Models
{
    public class ProductCompareModel
    {
        public List<Product> Products { get; set; }

    }

    public class ProductCompareJSONModel
    {
        public int productId { get; set; }
    }
}