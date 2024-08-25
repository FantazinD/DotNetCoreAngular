using api.DTOs.Feature;
using api.Models;

namespace api.Mappers
{
    public static class FeatureMapper
    {
        public static FeatureDTO ToFeatureDTO(this Feature feature)
        {
            return new FeatureDTO
            {
                Id = feature.Id,
                Name = feature.Name
            };
        }
    }
}