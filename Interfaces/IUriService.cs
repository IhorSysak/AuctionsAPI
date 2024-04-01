using AuctionsAPI.Contracts.Queries;

namespace AuctionsAPI.Interfaces
{
    public interface IUriService
    {
        Uri GetAllAuctionsUri(PaginationQuery pagination = null);
    }
}
