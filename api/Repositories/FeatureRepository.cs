using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class FeatureRepository(ApplicationDBContext context) : IFeatureRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<List<Feature>?> GetFeaturesAsync()
        {
            return await  _context.Features.ToListAsync();
        }
    }
}