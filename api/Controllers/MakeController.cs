using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/make")]
    [ApiController]
    public class MakeController(IMakeRepository makeRepository) : ControllerBase
    {
        private readonly IMakeRepository _makeRepository = makeRepository;

        [HttpGet("/api/makes")]
        public async Task<IActionResult> GetMakes()
        {
            var makes = await _makeRepository.GetMakesAsync();

            return Ok(makes?.Select(make => make.ToMakeDTO()).ToList());
        }
    }
}