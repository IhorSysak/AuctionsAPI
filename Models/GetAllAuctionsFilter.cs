using AuctionsAPI.Models.Enumerations;

namespace AuctionsAPI.Models
{
    public class GetAllAuctionsFilter
    {
        public string Name { get; set; }
        public string Seller { get; set; }
        public AuctionsStatus Status { get; set; }
        public string Sorting { get; set; }
    }
}
