using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Make;
using api.Models;

namespace api.Mappers
{
    public static class MakeMapper
    {
        public static MakeDTO ToStockDTO(this Make make)
        {
            return new MakeDTO
            {
                Id = make.Id,
                Name = make.Name,
                Models = make.Models.Select(model => model.ToModelDTO()).ToList()
            };
        }
    }
}