using api.DTOs.Photo;

namespace api.Interfaces
{
    public interface IPhotoStorage
    {
        Task<PhotoDTO> StorePhoto(string uploadsFolderPath, IFormFile file);
    }
}