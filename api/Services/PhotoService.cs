using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class PhotoService(IUnitOfWorkRepository unitOfWorkRepository, IPhotoStorage photoStorage) : IPhotoService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly IPhotoStorage _photoStorage = photoStorage;
        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath)
        {
            var fileName = await _photoStorage.StorePhoto(uploadsFolderPath, file);

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await _unitOfWorkRepository.CompleteAsync();

            //notificationSender class here

            return photo;
        }
    }
}