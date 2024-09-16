using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class VehicleRepository(ApplicationDBContext context) : IVehicleRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<Vehicle?> GetVehicleAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Vehicles
                    .Include(vehicle => vehicle.Features)
                    .SingleOrDefaultAsync(vehicle => vehicle.Id == id);

            return await _context.Vehicles
                .Include(vehicle => vehicle.Features)
                .ThenInclude(vehicleFeature => vehicleFeature.Feature)
                .Include(vehicle => vehicle.Model)
                .ThenInclude(vehicleModel => vehicleModel.Make)
                .SingleOrDefaultAsync(vehicle => vehicle.Id == id);
        }

        public void Add(Vehicle vehicle) 
        {
            _context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            _context.Remove(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(Filter filter)
        {
            var query = _context.Vehicles
                .Include(vehicle => vehicle.Model)
                .ThenInclude(vehicleModel => vehicleModel.Make)
                .Include(vehicle => vehicle.Features)
                .ThenInclude(vehicleFeature => vehicleFeature.Feature)
                .AsQueryable();

            if(filter.MakeId.HasValue){
                query = query.Where(vehicle => vehicle.Model.MakeId == filter.MakeId);
            };

            if(filter.ModelId.HasValue){
                query = query.Where(vehicle => vehicle.Model.Id == filter.ModelId);
            };

            return await query.ToListAsync();
        }
    }
}