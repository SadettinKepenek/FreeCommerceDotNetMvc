using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Brand:IEntity
    {
        
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandUrl { get; set; }
        public string BrandImageUrl { get; set; }
    }
}