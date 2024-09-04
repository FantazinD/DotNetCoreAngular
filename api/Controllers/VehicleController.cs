using api.Data;
using api.DTOs.Vehicle;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController(ApplicationDBContext context): ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        [HttpPost("/api/vehicles")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleDTO vehicleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newVehicle = vehicleDTO.ToVehicle();
            newVehicle.LastUpdate = DateTime.UtcNow;

            _context.Vehicles.Add(newVehicle); 
            await _context.SaveChangesAsync();

            newVehicle = await _context.Vehicles
                .Include(vehicle => vehicle.Features)
                .ThenInclude(vehicleFeature => vehicleFeature.Feature)
                .Include(vehicle => vehicle.Model)
                .ThenInclude(vehicleModel => vehicleModel.Make)
                .SingleOrDefaultAsync(vehicle => vehicle.Id == newVehicle.Id);

            return Ok(newVehicle.ToVehicleDTO());
        }

        [HttpPut("/api/vehicles/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleDTO vehicleDTO){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _context.Vehicles.Include(vehicle => vehicle.Features).SingleOrDefaultAsync(vehicle => vehicle.Id == id);

            if (vehicle == null)
                return NotFound();

            vehicle = vehicle.ToUpdatedVehicle(vehicleDTO);
            vehicle.LastUpdate = DateTime.Now;

            await _context.SaveChangesAsync();

            vehicle = await _context.Vehicles
                .Include(vehicle => vehicle.Features)
                .ThenInclude(vehicleFeature => vehicleFeature.Feature)
                .Include(vehicle => vehicle.Model)
                .ThenInclude(vehicleModel => vehicleModel.Make)
                .SingleOrDefaultAsync(vehicle => vehicle.Id == id);

            return Ok(vehicle!.ToVehicleDTO());
        }

        [HttpDelete("/api/vehicles/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            _context.Remove(vehicle);
            _context.SaveChangesAsync();

            return Ok(id);
        }

        [HttpGet("/api/vehicles/{id}")]
        public async Task<IActionResult> GetVehicle(int id){
            var vehicle = await _context.Vehicles
                .Include(vehicle => vehicle.Features)
                .ThenInclude(vehicleFeature => vehicleFeature.Feature)
                .Include(vehicle => vehicle.Model)
                .ThenInclude(vehicleModel => vehicleModel.Make)
                .SingleOrDefaultAsync(vehicle => vehicle.Id == id);

            if (vehicle == null)
                return NotFound();

            return Ok(vehicle.ToVehicleDTO());
        }
    }
}