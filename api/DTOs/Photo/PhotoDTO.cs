namespace api.DTOs.Photo
{
    public class PhotoDTO
    {
        public int? Id { get; set; }
        public string? FileName { get; set; } = string.Empty;
        public string? URL { get; set; } = string.Empty;
    }
}