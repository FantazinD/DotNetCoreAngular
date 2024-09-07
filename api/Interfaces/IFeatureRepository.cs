using api.Models;

namespace api.Interfaces
{
    public interface IFeatureRepository
    {
        Task<List<Feature>?> GetFeaturesAsync();
    }
}