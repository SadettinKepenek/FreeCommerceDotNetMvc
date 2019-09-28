using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeCommerceDotNet.Models.DbModels
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public string BrandUrl { get; set; }
        public string BrandImageUrl { get; set; }
    }
}