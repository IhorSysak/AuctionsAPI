using System.ComponentModel.DataAnnotations;

namespace AuctionsAPI.Contracts.Queries
{
    public class PaginationQuery
    {
        public PaginationQuery()
        {
            Cursor = 0;
            PageSize = 10;
        }

        public PaginationQuery(int cursor, int pageSize)
        {
            Cursor = cursor;
            PageSize = pageSize;
        }
        [Range(0, 100, ErrorMessage = "Cursor has to be between 0 and 100")]
        public int Cursor { get; set; }
        [Range(1, 100, ErrorMessage = "Page Size has to be between 1 and 100")]
        public int PageSize { get; set; }
    }
}
