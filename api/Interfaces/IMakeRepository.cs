using api.Models;

namespace api.Interfaces
{
    public interface IMakeRepository
    {
        Task<List<Make>?> GetMakesAsync(bool includeRelated = true);
    }
}