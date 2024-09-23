namespace api.Models
{
    public class PhotoSetting
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; } = Array.Empty<string>();
    }
}