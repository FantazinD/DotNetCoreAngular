using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Controllers;
using api.DTOs.Vehicle;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace api.UnitTests.Controllers
{
    public class VehicleControllerTests
    {
        private Mock<IVehicleRepository> _vehicleRepository;
        private Mock<IUnitOfWorkRepository> _unitOfWorkRepository;
        private VehicleController _vehicleController;
        private Vehicle _vehicle;
        private SaveVehicleDTO _saveVehicleDTO;

        [SetUp]
        public void SetUp()
        {
            _vehicle = new Vehicle()
            {
                Id = 1,
            };

            _saveVehicleDTO = new SaveVehicleDTO()
            {
                Id = 1,
            };

            _vehicleRepository = new Mock<IVehicleRepository>();
            _unitOfWorkRepository = new Mock<IUnitOfWorkRepository>();

            _vehicleController = new VehicleController(_vehicleRepository.Object, _unitOfWorkRepository.Object);
        }

        [Test]
        public async Task GetVehicle_NonExistentVehicle_ReturnNotFoundResult()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, true)).ReturnsAsync(() => null);

            var result = await _vehicleController.GetVehicle(_vehicle.Id);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task GetVehicle_ExistingVehicle_ReturnsVehicleObjectResult()
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

        [Test]
        public async Task DeleteVehicle_ModelStateIsNotValid_ReturnsBadRequest()
        {
            _vehicleController.ModelState.AddModelError("ContactName", "The ContactName field is required.");

            var result = await _vehicleController.DeleteVehicle(_vehicle.Id);
            var resultObj = result as BadRequestObjectResult;

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(resultObj?.Value, Is.TypeOf<SerializableError>());
        }

        [Test]
        public void DeleteVehicle_WhenCalled_GetsVehicleFromDatabase()
        {
            _vehicleController.DeleteVehicle(_vehicle.Id).Wait();

            _vehicleRepository.Verify(vr => vr.GetVehicleAsync(_vehicle.Id, false));
        }

        [Test]
        public async Task DeleteVehicle_NonExistentVehicle_ReturnsNotFound()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, false)).ReturnsAsync(() => null);

            var result = await _vehicleController.DeleteVehicle(_vehicle.Id);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void DeleteVehicle_ExistingVehicle_DeletesVehicleFromDatabase()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, false)).ReturnsAsync(_vehicle);

            _vehicleController.DeleteVehicle(_vehicle.Id).Wait();

            _vehicleRepository.Verify(vr => vr.Remove(_vehicle));
        }

        [Test]
        public async Task DeleteVehicle_VehicleDeleted_ReturnsVehicleIdObjectResult()
        {
            _vehicleRepository.Setup(vr => vr.GetVehicleAsync(_vehicle.Id, false)).ReturnsAsync(_vehicle);

            var result = await _vehicleController.DeleteVehicle(_vehicle.Id);
            var resultObj = result as OkObjectResult;

            Assert.That(resultObj.Value, Is.EqualTo(_vehicle.Id));
        }

        [Test]
        public async Task UpdateVehicle_ModelStateIsNotValid_ReturnsBadRequest()
        {
            _vehicleController.ModelState.AddModelError("ContactName", "The ContactName field is required.");

            var result = await _vehicleController.UpdateVehicle(_vehicle.Id, _saveVehicleDTO);
            var resultObj = result as BadRequestObjectResult;

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(resultObj?.Value, Is.TypeOf<SerializableError>());
        }
    }
}
