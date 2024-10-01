using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PhotoRepository(ApplicationDBContext context) : IPhotoRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<IEnumerable<Photo>?> GetPhotos(int vehicleId) 
        {
            return await _context.Photos.Where(photo => photo.VehicleId == vehicleId).ToListAsync();
        }
    }
}