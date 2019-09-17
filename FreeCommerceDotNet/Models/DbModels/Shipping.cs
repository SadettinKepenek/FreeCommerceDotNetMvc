namespace FreeCommerceDotNet.Models.DbModels
{
    public class Shipping
    {
        /// Relationship yok
        /// Direk veritabanındaki propertyler eklenecek.
        /// 

        private int _ShippingId;

        public int ShippingId
        {
            get { return _ShippingId; }
            set { _ShippingId = value; }
        }

        private string _ShippingName;

        public string ShippingName
        {
            get { return _ShippingName; }
            set { _ShippingName = value; }
        }

        private string _ShippingDescription;

        public string ShippingDescription
        {
            get { return _ShippingDescription; }
            set { _ShippingDescription = value; }
        }


    }
}