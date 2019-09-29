namespace FreeCommerceDotNet.Models.DbModels
{
    public class Customer
    {
        /// Orders
        /// Reviews
        /// Kendi Bilgileri
        /// ... Payments Invoices
        /// Shippings
        /// 

        public int CustomerId { get; set; }
        public string Firstname { get; set; }

        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public string TaxAddress { get; set; }
        public bool Status { get; set; }

        public int SegmentId { get; set; }
        public int UserId { get; set; }

    }
}