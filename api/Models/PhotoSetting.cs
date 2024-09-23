namespace api.Models
{
    public class PhotoSetting
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileExtensions { get; set; } = Array.Empty<string>();
        public bool IsSupported(string fileName)
        {
            return AcceptedFileExtensions.Any(acceptedFileExtension => acceptedFileExtension == Path.GetExtension(fileName).ToLower());
        }
    }
}