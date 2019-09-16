namespace FreeCommerceDotNet.Models
{
    public class ProductPrices
    {
        
        private int _product;

        public int Product
        {
            get { return _product; }
            set { _product = value; }
        }

        private int _priceId;

        public int PriceId
        {
            get { return _priceId; }
            set { _priceId = value; }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private string _segment;

        public string Segment
        {
            get { return _segment; }
            set { _segment = value; }
        }


    }
}