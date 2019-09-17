namespace FreeCommerceDotNet.Models.DbModels
{
    public class Reviews
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
    }
}