using System.Linq.Expressions;
using api.Data;
using api.Extensions.IQueryableExtensions;
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

        public async Task<QueryResult<Vehicle>> GetVehiclesAsync(VehicleQuery vehicleQuery)
        {
            var result = new QueryResult<Vehicle>();

            var query = _context.Vehicles
                .Include(vehicle => vehicle.Model)
                    .ThenInclude(vehicleModel => vehicleModel.Make)
                .Include(vehicle => vehicle.Features)
                    .ThenInclude(vehicleFeature => vehicleFeature.Feature)
                .AsQueryable();

            if(vehicleQuery.MakeId.HasValue){
                query = query.Where(vehicle => vehicle.Model.MakeId == vehicleQuery.MakeId);
            };

            if(vehicleQuery.ModelId.HasValue){
                query = query.Where(vehicle => vehicle.Model.Id == vehicleQuery.ModelId);
            };

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>(){
                ["make"] = vehicle => vehicle.Model.Make.Name,
                ["model"] = vehicle => vehicle.Model.Name,
                ["contactName"] = vehicle => vehicle.ContactName
            };

            query = query.ApplyOrdering(vehicleQuery, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(vehicleQuery);

            result.Items = await query.ToListAsync();

            return result;
        }
    }
}