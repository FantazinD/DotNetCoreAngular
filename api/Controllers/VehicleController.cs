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

        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] VehicleDTO vehicleDTO){
            return Ok();
        }
    }
}