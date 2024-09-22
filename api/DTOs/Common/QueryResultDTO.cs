namespace api.DTOs.Common
{
    public class QueryResultDTO<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; } = [];
    }
}