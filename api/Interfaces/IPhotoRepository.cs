using api.Models;

namespace api.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>?> GetPhotos(int vehicleId);
    }
}