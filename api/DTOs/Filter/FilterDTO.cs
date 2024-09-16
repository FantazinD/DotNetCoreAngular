namespace api.DTOs.Filter
{
    public class FilterDTO
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public bool IsSortAscending { get; set; }
    }
}