using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Route("api/vehicles/{vehicleId}/photos")]
    [ApiController]
    public class PhotoController(IVehicleRepository vehicleRepository, IConfiguration configuration, IPhotoRepository photoRepository, IPhotoService photoService):ControllerBase
    {
        private readonly PhotoSetting _photoSettings = configuration.GetSection("PhotoSettings").Get<PhotoSetting>();
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IPhotoRepository _photoRepository = photoRepository;
        private readonly IPhotoService _photoService = photoService;

        [HttpPost]
        public async Task<IActionResult> Upload([FromRoute] int vehicleId, IFormFile file)
        {
            var vehicle = await _vehicleRepository.GetVehicleAsync(vehicleId, includeRelated: false);

            if(vehicle == null)
                return NotFound();

            if(file == null) return BadRequest("Null File.");
            if(file.Length == 0) return BadRequest("Empty File.");
            if(file.Length > _photoSettings.MaxBytes) return BadRequest("Maximum file size exceeded.");
            if(!_photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var photo = await _photoService.UploadPhoto(vehicle, file);

            return Ok(photo.ToPhotoDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int vehicleId)
        {   
            var photos = await _photoRepository.GetPhotos(vehicleId);

            return Ok(photos.Select(photo => photo.ToPhotoDTO()));
        }
    }
}