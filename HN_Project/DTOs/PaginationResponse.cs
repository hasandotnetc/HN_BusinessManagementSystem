namespace HN_Backend.DTOs
{
    public class PaginationResponse<T>
    {

        public IEnumerable<T> Data { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
