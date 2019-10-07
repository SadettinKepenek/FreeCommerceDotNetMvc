using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Brand:IEntity
    {
        
        public int BrandId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage ="Brand Adı Boş Olamaz" )]
        public string BrandName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Brand Açıklaması Boş Olamaz")]
        public string BrandDescription { get; set; }
        public string BrandUrl { get; set; }
        public string BrandImageUrl { get; set; }
    }
}