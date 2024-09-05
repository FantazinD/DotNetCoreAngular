using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class MakeRepository(ApplicationDBContext context) : IMakeRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<List<Make>?> GetMakesAsync(bool includeRelated = true)
        {
            if(!includeRelated)
                return await _context.Makes.ToListAsync();

            return await _context.Makes.Include(make => make.Models).ToListAsync();
        }
    }
}