using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Controllers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.UnitTests.Controllers
{
    public class VehicleControllerTests
    {
        private Mock<IVehicleRepository> _vehicleRepository;
        private Mock<IUnitOfWorkRepository> _unitOfWorkRepository;
        private VehicleController _vehicleController;
        private Vehicle _vehicle;

        [SetUp]
        public void SetUp()
        {
            _vehicle = new Vehicle()
            {
                Id = 1,
            };

            _vehicleRepository = new Mock<IVehicleRepository>();
            _unitOfWorkRepository = new Mock<IUnitOfWorkRepository>();

            _vehicleController = new VehicleController(_vehicleRepository.Object, _unitOfWorkRepository.Object);
        }

        [Test]
        public async Task GetVehicle_VehicleIsNull_ReturnNotFoundResult()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, true)).ReturnsAsync(() => null);

            var result = await _vehicleController.GetVehicle(_vehicle.Id);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task GetVehicle_ExistingVehicle_ReturnsVehicleObject()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, true)).ReturnsAsync(new Vehicle()
            {
                Id = 1,
            });

            var result = await _vehicleController.GetVehicle(_vehicle.Id);
            var objResult = ((OkObjectResult)result).Value;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(
                objResult?
                .GetType()
                .GetProperty("Id")?
                .GetValue(objResult), 
                Is.EqualTo(1));
        }
    }
}
