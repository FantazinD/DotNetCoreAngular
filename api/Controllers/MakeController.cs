using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/make")]
    [ApiController]
    public class MakeController(ApplicationDBContext context) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        [HttpGet("/api/makes")]
        public async Task<IActionResult> GetMakes()
        {
            var makes = await _context.Makes.Include(make => make.Models).ToListAsync();

            return Ok(makes.Select(make => make.ToMakeDTO()).ToList());
        }
    }
}