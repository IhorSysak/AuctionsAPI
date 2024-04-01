using AuctionsAPI.Models.Enumerations;

namespace AuctionsAPI.Contracts.Responses
{
    public class SaleResponce
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public AuctionsResponse ItemResponse { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime FinishedDt { get; set; }
        public decimal Price { get; set; }
        public AuctionsStatus Status { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}
