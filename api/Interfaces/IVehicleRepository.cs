using api.Models;

namespace api.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetVehicleAsync(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    }
}