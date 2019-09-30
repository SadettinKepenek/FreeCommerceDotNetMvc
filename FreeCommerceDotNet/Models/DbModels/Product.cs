using System.Collections.Generic;

namespace FreeCommerceDotNet.Models.DbModels
{
    public class Product
    {
        private int _productId;

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private int _categoryId;

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private string _productDescription;

        public string ProductDescription
        {
            get { return _productDescription; }
            set { _productDescription = value; }
        }

        private string _metatagTitle;

        public string MetatagTitle
        {
            get { return _metatagTitle; }
            set { _metatagTitle = value; }
        }

        private string _metatagDescription;

        public string MetatagDescription
        {
            get { return _metatagDescription; }
            set { _metatagDescription = value; }
        }

        private string _metatagKeywords;

        public string MetatagKeywords
        {
            get { return _metatagKeywords; }
            set { _metatagKeywords = value; }
        }



        private string _productTags;

        public string ProductTags
        {
            get { return _productTags; }
            set { _productTags = value; }
        }

        private string _productCode;

        public string ProductCode
        {
            get { return _productCode; }
            set { _productCode = value; }
        }

        private string _sku;

        public string SKU
        {
            get { return _sku; }
            set { _sku = value; }
        }

        private string _upc;

        public string UPC
        {
            get { return _upc; }
            set { _upc = value; }
        }

        private string _ean;

        public string EAN
        {
            get { return _ean; }
            set { _ean = value; }
        }

        private string _jan;

        public string JAN
        {
            get { return _jan; }
            set { _jan = value; }
        }

        private string _isbn;

        public string ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        private string _mpn;

        public string MPN
        {
            get { return _mpn; }
            set { _mpn = value; }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        private string _outofStockStatus;

        public string OutofStockStatus
        {
            get { return _outofStockStatus; }
            set { _outofStockStatus = value; }
        }

        private string _availableDate;

        public string AvailableDate
        {
            get { return _availableDate; }
            set { _availableDate = value; }
        }

        private double _length;

        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        private double _weight;

        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        private bool _status;

        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _brand;

        public int Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }


        
        
    }
}