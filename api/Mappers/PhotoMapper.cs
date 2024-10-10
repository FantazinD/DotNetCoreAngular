using api.DTOs.Photo;
using api.Models;

namespace api.Mappers
{
    public static class PhotoMapper
    {
        public static PhotoDTO ToPhotoDTO(this Photo photo){
            return new PhotoDTO {
                FileName = photo.FileName,
                URL = photo.URL,
                Id = photo.Id
            };
        }
    }
}