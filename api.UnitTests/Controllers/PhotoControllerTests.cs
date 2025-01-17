using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Controllers;
using api.Interfaces;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace api.UnitTests.Controllers
{
    public class PhotoControllerTests
    {
        private Mock<IOptionsSnapshot<PhotoSetting>> _photoSettings;
        private Mock<IWebHostEnvironment> _host;
        private Mock<IVehicleRepository> _vehicleRepository;
        private Mock<IPhotoRepository> _photoRepository;
        private Mock<IPhotoService> _photoService;
        private Mock<IFormFile> _file;
        private Vehicle _vehicle;
        private PhotoController _photoController;

        [SetUp]
        public void SetUp()
        {
            _vehicle = new Vehicle()
            {
                Id = 1,
            };

            byte[] jpgContent = new byte[] { 255, 216, 255, 224, 0, 16, 74, 70, 73, 70, 0, 1, 0, 1, 0, 0, 0, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 };

            _file = new Mock<IFormFile>();
            _file.Setup(f => f.FileName).Returns("MockFile.jpg");
            _file.Setup(f => f.Length).Returns(jpgContent.Length);
            _file.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(jpgContent));
            _file.Setup(f => f.CopyTo(It.IsAny<Stream>())).Callback<Stream>((stream) =>
            {
                stream.Write(jpgContent, 0, jpgContent.Length);
            });

            _host = new Mock<IWebHostEnvironment>();
            _vehicleRepository = new Mock<IVehicleRepository>();
            _photoSettings = new Mock<IOptionsSnapshot<PhotoSetting>>();
            _photoRepository = new Mock<IPhotoRepository>();
            _photoRepository.Setup(pr => pr.GetPhotos(_vehicle.Id)).ReturnsAsync(new List<Photo>());

            _photoService = new Mock<IPhotoService>();

            _photoController = new PhotoController(
                _host.Object, 
                _vehicleRepository.Object, 
                _photoSettings.Object, 
                _photoRepository.Object, 
                _photoService.Object);
        }

        [Test]
        public void GetPhotos_WhenCalled_GetsPhotosFromDatabase()
        {
            _photoController.GetPhotos(_vehicle.Id).Wait();

            _photoRepository.Verify(pr => pr.GetPhotos(_vehicle.Id));
        }

        [Test]
        public async Task GetPhotos_WhenCalled_ReturnsIActionResultInstance()
        {
            var result = await _photoController.GetPhotos(new Vehicle() { Id = 1 }.Id);

            Assert.That(result, Is.InstanceOf<IActionResult>());
        }

        [Test]
        public async Task Upload_VehicleIsNull_ReturnsNotFoundResult()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, false)).ReturnsAsync(() => null);
            var result = await _photoController.Upload(_vehicle.Id, It.IsAny<IFormFile>());

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task Upload_FileIsNull_ReturnsBadRequestNullFileMessage()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, false)).ReturnsAsync(new Vehicle());
            IFormFile file = null;

            var result = await _photoController.Upload(_vehicle.Id, file);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Null File.").IgnoreCase);
        }

        [Test]
        public async Task Upload_FileIsEmpty_ReturnsBadRequestEmptyFileMessage()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, false)).ReturnsAsync(new Vehicle());
            _file.Setup(f => f.Length).Returns(0);

            var result = await _photoController.Upload(_vehicle.Id, _file.Object);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Empty File.").IgnoreCase);
        }
    }
}
