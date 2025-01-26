using api.DTOs.Photo;

namespace api.Interfaces
{
    public interface IPhotoStorage
    {
        Task<PhotoDTO> StorePhoto(IFormFile file);
    }
}