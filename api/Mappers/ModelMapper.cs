using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Model;
using api.Models;

namespace api.Mappers
{
    public static class ModelMapper
    {
        public static ModelDTO ToModelDTO(this Model model)
        {
            return new ModelDTO
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}