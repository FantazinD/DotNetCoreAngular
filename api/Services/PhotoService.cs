using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class PhotoService(IUnitOfWorkRepository unitOfWorkRepository, IPhotoStorage photoStorage) : IPhotoService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        private readonly IPhotoStorage _photoStorage = photoStorage;
        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file)
        {
            var photoDTO = await _photoStorage.StorePhoto(file);

            var photo = new Photo { 
                FileName = photoDTO.FileName!,
                URL = photoDTO.URL! 
            };
            vehicle.Photos.Add(photo);
            await _unitOfWorkRepository.CompleteAsync();

            //notificationSender class here

            return photo;
        }
    }
}