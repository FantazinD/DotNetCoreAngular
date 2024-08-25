using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Route("api/feature")]
    [ApiController]
    public class FeatureController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        [HttpGet("/api/features")]
        public async Task<IActionResult> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();
        
            return Ok(features.Select(feature => feature.ToFeatureDTO()).ToList());
        }
    }
}