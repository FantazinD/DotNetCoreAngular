using api.DTOs.Common;
using api.Models;

namespace api.Mappers
{
    public static class ModelMapper
    {
        public static IdNameObjectDTO ToIdNameObjectDTO(this Model model)
        {
            return new IdNameObjectDTO
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}