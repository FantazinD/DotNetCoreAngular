using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Make;
using api.Mappers;
using api.Models;
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
            var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

            return Ok(makes.Select(make => make.ToMakeDTO()).ToList());
        }
    }
}