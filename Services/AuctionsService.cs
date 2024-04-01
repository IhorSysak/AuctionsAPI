using AuctionsAPI.Context;
using AuctionsAPI.Interfaces;
using AuctionsAPI.Models;
using AuctionsAPI.Models.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace AuctionsAPI.Services
{
    public class AuctionsService : IAuctionsService
    {
        private readonly AuctionsContext _auctionsContext;

        public AuctionsService(AuctionsContext auctionsContext) 
        {
            _auctionsContext = auctionsContext;
        }

        public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellation, GetAllAuctionsFilter filter = null, PaginationFilter paginationFilter = null)
        {
            var queryble = _auctionsContext.Items.AsQueryable();

            if (paginationFilter == null) 
            {
                return await queryble.ToListAsync(cancellation);
            }

            queryble = AddFiltersOnQuery(filter, queryble);

            queryble = SortingByField(filter, queryble);

             return await queryble.Where(e => e.Id > paginationFilter.Cursor).Take(paginationFilter.PageSize).ToListAsync(cancellation);
        }

        public async Task<Item> GetAsync(int id, CancellationToken cancellation)
        {
            return await _auctionsContext.Items.FindAsync(id, cancellation);
        }

        private static IQueryable<Item> AddFiltersOnQuery(GetAllAuctionsFilter filter, IQueryable<Item> queryble)
        {
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryble = queryble.Where(x => x.Name == filter.Name);
            }

            if (!string.IsNullOrEmpty(filter?.Seller))
            {
                queryble = queryble.Where(x => x.Sale.Seller == filter.Seller);
            }

            if (filter?.Status != null && filter?.Status != AuctionsStatus.None)
            {
                queryble = queryble.Where(x => x.Sale.Status == filter.Status);
            }

            return queryble;
        }

        private IQueryable<Item> SortingByField(GetAllAuctionsFilter filter, IQueryable<Item> queryble) 
        {
            switch (filter?.Sorting) 
            {
                case "CreateDt_DESC":
                    queryble = queryble.OrderByDescending(o => o.Sale.CreateDt);
                    break;
                case "CreateDt":
                    queryble = queryble.OrderBy(o => o.Sale.CreateDt);
                    break;
                case "Price_DESC":
                    queryble =  queryble.OrderByDescending(o => o.Sale.Price);
                    break;
                case "Price":
                    queryble = queryble.OrderBy(o => o.Sale.Price);
                    break;
                default:
                    queryble = queryble.OrderBy(o => o.Id);
                    break;
            }

            return queryble;
        }
    }
}
