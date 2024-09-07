using api.Data;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Route("api/feature")]
    [ApiController]
    public class FeatureController(IFeatureRepository featureRepository) : ControllerBase
    {
        private readonly IFeatureRepository _featureRepository = featureRepository;

        [HttpGet("/api/features")]
        public async Task<IActionResult> GetFeatures()
        {
            var features = await _featureRepository.GetFeaturesAsync();
        
            return Ok(features?.Select(feature => feature.ToIdNameObjectDTO()).ToList());
        }
    }
}