using AuctionsAPI.Contracts.Queries;
using AuctionsAPI.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace AuctionsAPI.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri) 
        {
            _baseUri = baseUri;
        }

        public Uri GetAllAuctionsUri(PaginationQuery pagination = null) 
        {
            var uri = new Uri(_baseUri);

            if (pagination == null) 
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.Cursor.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}
