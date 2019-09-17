namespace FreeCommerceDotNet.Models.DbModels
{
    public class Store
    {
        /// Relationship yok
        /// Direk veritabanındaki propertyler eklenecek.

        private int _StoreId;

        public int StoreId
        {
            get { return _StoreId; }
            set { _StoreId = value; }
        }
        private string _MetaTitle;

        public string MetaTitle
        {
            get { return _MetaTitle; }
            set { _MetaTitle = value; }
        }

        private string _MetaTagDescription;

        public string MetaTagDescription
        {
            get { return _MetaTagDescription; }
            set { _MetaTagDescription = value; }
        }

        private string _MetaTagKeywords;

        public string MetaTagKeywords
        {
            get { return MetaTagKeywords; }
            set { MetaTagKeywords = value; }
        }
        private string _StoreName;

        public string StoreName
        {
            get { return _StoreName; }
            set { _StoreName = value; }
        }
        private string _StoreOwner;

        public string StoreOwner
        {
            get { return _StoreOwner; }
            set { _StoreOwner = value; }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _EMail;

        public string Email
        {
            get { return _EMail; }
            set { _EMail = value; }
        }

        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _CellPhone;

        public string CellPhone
        {
            get { return _CellPhone; }
            set { _CellPhone = value; }
        }

        private string _Fax;

        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        private string _ImageUrl;

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        private string _OpeningTimes;

        public string OpeningTimes
        {
            get { return _OpeningTimes; }
            set { _OpeningTimes = value; }
        }

        private string _Comment;

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }

        private bool _AllowReviews;

        public bool AllowReviews
        {
            get { return _AllowReviews; }
            set { _AllowReviews = value; }
        }

        private bool _DisplayPricesWithTax;

        public bool DisplayPricesWithTax
        {
            get { return _DisplayPricesWithTax; }
            set { _DisplayPricesWithTax = value; }
        }

        private bool _LoginDisplayPrices;

        public bool LoginDisplayPrices
        {
            get { return _LoginDisplayPrices; }
            set { _LoginDisplayPrices = value; }
        }

        private bool _MaxLoginAttempts;

        public bool MaxLoginAttempts
        {
            get { return _MaxLoginAttempts; }
            set { _MaxLoginAttempts = value; }
        }

        private bool _DisplayStock;

        public bool DisplayStock
        {
            get { return _DisplayStock; }
            set { _DisplayStock = value; }
        }

        private bool _ShowOutOfStockWarning;

        public bool ShowOutOfStockWarning
        {
            get { return _ShowOutOfStockWarning; }
            set { _ShowOutOfStockWarning = value; }
        }


    }
}