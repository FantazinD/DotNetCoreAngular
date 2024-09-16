namespace api.Models
{
    public class Filter
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public bool IsSortAscending { get; set; }
    }
}