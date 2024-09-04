using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class VehicleRepository(ApplicationDBContext context) : IVehicleRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task<Vehicle?> GetVehicle(int id, bool includeRelated = true)
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
    }
}