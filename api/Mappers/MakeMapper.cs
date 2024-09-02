using api.DTOs.Common;
using api.DTOs.Make;
using api.Models;

namespace api.Mappers
{
    public static class MakeMapper
    {
        public static MakeDTO ToMakeDTO(this Make make)
        {
            return new MakeDTO
            {
                Id = make.Id,
                Name = make.Name,
                Models = make.Models.Select(model => model.ToIdNameObjectDTO()).ToList()
            };
        }

        public static IdNameObjectDTO ToIdNameObjectDTO(this Make make){
            return new IdNameObjectDTO{
                Id = make.Id,
                Name = make.Name
            };
        }
    }
}