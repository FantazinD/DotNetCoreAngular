using api.Data;
using api.DTOs.Vehicle;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    //delet dis - rename to VehicleController
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController(ApplicationDBContext context): ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        [HttpPost("/api/vehicles")]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleDTO vehicleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = vehicleDTO.ToVehicle();
            vehicle.LastUpdate = DateTime.UtcNow;

            _context.Vehicles.Add(vehicle); 
            await _context.SaveChangesAsync();

            return Ok(vehicle);
        }

        [HttpPut("/api/vehicles/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleDTO vehicleDTO){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await context.Vehicles.Include(vehicle => vehicle.Features).SingleOrDefaultAsync(vehicle => vehicle.Id == id);

            if (vehicle == null)
                return BadRequest("Vehicle not found.");

            var updatedVehicle = vehicle.UpdateVehicle(vehicleDTO);
            updatedVehicle.LastUpdate = DateTime.Now;

            //await _context.SaveChangesAsync();

            return Ok(updatedVehicle);
        }
    }
}