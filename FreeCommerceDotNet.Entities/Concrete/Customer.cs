using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Customer:IEntity
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Firstname boş geçilemez")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname boş geçilemez")]
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Password boş geçilemez")]
        public string Password { get; set; }
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public string TaxAddress { get; set; }
        public bool Status { get; set; }

        public int SegmentId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Segment boş geçilemez")]
        public Segment Segment { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<OrderMaster> OrderMasters { get; set; }
        public List<OrderReturn> OrderReturns { get; set; }
        public User User { get; set; }

    }
}