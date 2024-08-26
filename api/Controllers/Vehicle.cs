using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    //delet dis - rename to VehicleController
    [Route("api/vehicle")]
    [ApiController]
    public class Vehicle(ApplicationDBContext context): ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        [HttpGet("/api/vehicles")]
        public async Task<IActionResult> GetVehicles()
        {
            return Ok();
        }
    }
}