using System.Collections.ObjectModel;
using api.DTOs.Model;

namespace api.DTOs.Make
{
    public class MakeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ModelDTO> Models { get; set; } = new Collection<ModelDTO>();
    }
}