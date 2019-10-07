using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FreeCommerceDotNet.Entities.Abstract;

namespace FreeCommerceDotNet.Entities.Concrete
{
    public class Payment:IEntity
    {
        private int _PaymentId;

        public int PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }

        private string _PaymentName;
        [Required(ErrorMessage = "Payment Adı Zorunludur")]
        public string PaymentName
        {
            get { return _PaymentName; }
            set { _PaymentName = value; }
        }

        private string _PaymentDescription;
        [Required(ErrorMessage = "Payment Description Zorunludur")]
        public string PaymentDescription
        {
            get { return _PaymentDescription; }
            set { _PaymentDescription = value; }
        }

        public List<OrderMaster> OrderMasterBms { get; set; }

    }
}