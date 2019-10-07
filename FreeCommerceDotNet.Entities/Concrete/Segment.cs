using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Segment:IEntity
    {
        public int SegmentId { get; set; }

        [Required(ErrorMessage = "SegmentName  boş geçilemez")]
        public string SegmentName { get; set; }
        public string Priorty { get; set; }
        public List<Customer> CustomersBms { get; set; }

    }
}