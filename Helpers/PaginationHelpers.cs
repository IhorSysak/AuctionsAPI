using AuctionsAPI.Contracts.Responses;
using AuctionsAPI.Contracts.Queries;
using AuctionsAPI.Interfaces;
using AuctionsAPI.Models;

namespace AuctionsAPI.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponce<T> CreatePaginatedResponce<T>(IUriService uriService, PaginationFilter pagination, IEnumerable<T> responce)
        {
            var nextCursor = pagination.Cursor >= 1 ? uriService
                .GetAllAuctionsUri(new PaginationQuery(pagination.Cursor + 1, pagination.PageSize)).ToString() : null;

            var previousPage = pagination.Cursor - 1 >= 1 ? uriService
               .GetAllAuctionsUri(new PaginationQuery(pagination.Cursor - 1, pagination.PageSize)).ToString() : null;

            return new PagedResponce<T>()
            {
                Data = responce,
                PageNumber = pagination.Cursor >= 1 ? pagination.Cursor : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                NextPage = responce.Any() ? nextCursor : null,
                PreviousPage = previousPage
            };
        }
    }
}
