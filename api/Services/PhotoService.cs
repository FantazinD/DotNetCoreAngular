using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class PhotoService(IUnitOfWorkRepository unitOfWorkRepository) : IPhotoService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;
        public async Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath)
        {
            
            if(!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await _unitOfWorkRepository.CompleteAsync();

            return photo;
        }
    }
}