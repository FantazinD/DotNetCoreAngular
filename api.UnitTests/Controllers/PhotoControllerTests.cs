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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace api.UnitTests.Controllers
{
    public class PhotoControllerTests
    {
        private Mock<IWebHostEnvironment> _host;
        private Mock<IVehicleRepository> _vehicleRepository;
        private Mock<IPhotoRepository> _photoRepository;
        private Mock<IPhotoService> _photoService;
        private Mock<IFormFile> _file;
        private IConfiguration _configuration;
        private PhotoSetting _photoSetting;
        private Vehicle _vehicle;
        private PhotoController _photoController;
        
        public PhotoControllerTests()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = configurationBuilder.Build();
        }

        [SetUp]
        public void SetUp()
        {
            _photoSetting = _configuration.GetSection("PhotoSettings").Get<PhotoSetting>();

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
            
            _vehicleRepository = new Mock<IVehicleRepository>();
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, false)).ReturnsAsync(new Vehicle());

            _host = new Mock<IWebHostEnvironment>();
            _photoRepository = new Mock<IPhotoRepository>();
            _photoRepository.Setup(pr => pr.GetPhotos(_vehicle.Id)).ReturnsAsync(new List<Photo>());

            _photoService = new Mock<IPhotoService>();

            _photoController = new PhotoController(
                _host.Object, 
                _vehicleRepository.Object,
                _configuration, 
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
        public async Task Upload_FileIsNull_ReturnsBadRequestObjectResult()
        {
            var result = await _photoController.Upload(_vehicle.Id, file: null);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Null File.").IgnoreCase);
        }

        [Test]
        public async Task Upload_FileIsEmpty_ReturnsBadRequestObjectResult()
        {
            _file.Setup(f => f.Length).Returns(0);

            var result = await _photoController.Upload(_vehicle.Id, _file.Object);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Empty File.").IgnoreCase);
        }

        [Test]
        public async Task Upload_FileExceedsMaxBytes_ReturnsBadRequestObjectResult()
        {
            _file.Setup(f => f.Length).Returns(_photoSetting.MaxBytes + 1);

            var result = await _photoController.Upload(_vehicle.Id, _file.Object);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Maximum file size exceeded.").IgnoreCase);
        }

        [Test]
        public async Task Upload_FileTypeNotSupported_ReturnsBadRequestObjectResult()
        {
            _file.Setup(f => f.FileName).Returns("MockFile.txt");

            var result = await _photoController.Upload(_vehicle.Id, _file.Object);

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Invalid file type.").IgnoreCase);
        }
    }
}
