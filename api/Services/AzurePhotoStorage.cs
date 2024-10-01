using api.Interfaces;

namespace api.Services
{
    public class AzurePhotoStorage : IPhotoStorage
    {
        public Task<string> StorePhoto(string uploadsFolderPath, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}