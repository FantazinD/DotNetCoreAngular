using api.DTOs.Common;
using api.Models;

namespace api.Mappers
{
    public static class FeatureMapper
    {
        public static IdNameObjectDTO ToIdNameObjectDTO(this Feature feature)
        {
            return new IdNameObjectDTO
            {
                Id = feature.Id,
                Name = feature.Name
            };
        }
    }
}