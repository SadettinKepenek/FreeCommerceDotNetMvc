namespace FreeCommerceDotNet.Models.DbModels
{
    public class Payment
    {
        /// Relationship yok
        /// Direk veritabanındaki propertyler eklenecek.
        /// 
        private int _PaymentId;

        public int PaymentId
        {
            get { return _PaymentId; }
            set { _PaymentId = value; }
        }

        private string _PaymentName;

        public string PaymentName
        {
            get { return _PaymentName; }
            set { _PaymentName = value; }
        }

        private string _PaymentDescription;

        public string PaymentDescription
        {
            get { return _PaymentDescription; }
            set { _PaymentDescription = value; }
        }

    }
}