using AuctionsAPI.Models.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace AuctionsAPI.Contracts.Queries
{
    public class GetAllAuctionsQuery
    {
        [FromQuery(Name = "Name")]
        public string? Name { get; set; }
        [FromQuery(Name = "Seller")]
        public string? Seller { get; set; }
        [FromQuery(Name = "Status")]
        public AuctionsStatus Status { get; set; }
        [FromQuery(Name = "Sorting")]
        public string? Sorting { get; set; }
    }
}
