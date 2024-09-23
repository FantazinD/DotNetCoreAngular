using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Route("api/vehicles/{vehicleId}/photos")]
    [ApiController]
    public class PhotoController(IWebHostEnvironment host, IVehicleRepository vehicleRepository, IUnitOfWorkRepository unitOfWorkRepository, IOptionsSnapshot<PhotoSetting> optionsSnapshot, IPhotoRepository photoRepository):ControllerBase
    {
        private readonly PhotoSetting _photoSettings = optionsSnapshot.Value;
        private readonly IWebHostEnvironment _host = host;
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IPhotoRepository _photoRepository = photoRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

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

            var appPath = _host.ContentRootPath.Substring(0,_host.ContentRootPath.Length - string.Concat("/", _host.ApplicationName).Length);
            var uploadsFolderPath = string.Concat(appPath, "/clientapp/public/uploads");
            
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