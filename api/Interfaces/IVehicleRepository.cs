using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetVehicle(int id, bool includeRelated = true);
    }
}