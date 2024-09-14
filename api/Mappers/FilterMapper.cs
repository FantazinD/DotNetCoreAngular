using api.DTOs.Filter;
using api.Models;

namespace api.Mappers
{
    public static class FilterMapper
    {
        public static Filter ToFilter(this FilterDTO filterDTO){
            return new Filter{
                MakeId = 0
            };
        }
    }
}