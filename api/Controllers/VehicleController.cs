using api.DTOs.Vehicle;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController(IVehicleRepository vehicleRepository, IUnitOfWorkRepository unitOfWorkRepository): ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository = unitOfWorkRepository;

        [HttpPost("/api/vehicles")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleDTO vehicleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newVehicle = vehicleDTO.ToVehicle();
            newVehicle.LastUpdate = DateTime.UtcNow;

            _vehicleRepository.Add(newVehicle);
            await _unitOfWorkRepository.CompleteAsync();

            newVehicle = await _vehicleRepository.GetVehicleAsync(newVehicle.Id);

            return Ok(newVehicle!.ToVehicleDTO());
        }

        [HttpPut("/api/vehicles/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleDTO vehicleDTO){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _vehicleRepository.GetVehicleAsync(id, includeRelated: false);
            
            if (vehicle == null)
                return NotFound();

            vehicle = vehicle.ToUpdatedVehicle(vehicleDTO);
            vehicle.LastUpdate = DateTime.Now;

            await _unitOfWorkRepository.CompleteAsync();

            vehicle = await _vehicleRepository.GetVehicleAsync(id);

            return Ok(vehicle!.ToVehicleDTO());
        }

        [HttpDelete("/api/vehicles/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _vehicleRepository.GetVehicleAsync(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            _vehicleRepository.Remove(vehicle);
            await _unitOfWorkRepository.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("/api/vehicles/{id}")]
        public async Task<IActionResult> GetVehicle(int id){
            var vehicle = await _vehicleRepository.GetVehicleAsync(id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle.ToVehicleDTO());
        }
    }
}