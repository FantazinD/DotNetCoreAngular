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
        private Vehicle _vehicle;
        private PhotoController _photoController;

        [SetUp]
        public void SetUp()
        {
            _vehicle = new Vehicle()
            {
                Id = 1
            };
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
    }
}
