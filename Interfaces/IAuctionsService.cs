using AuctionsAPI.Models;

namespace AuctionsAPI.Interfaces
{
    public interface IAuctionsService
    {
        Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellation, GetAllAuctionsFilter filter = null, PaginationFilter paginationFilter = null);
        Task<Item> GetAsync(int id, CancellationToken cancellation);
    }
}
