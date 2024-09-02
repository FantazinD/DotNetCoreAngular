using System.Collections.ObjectModel;
using api.DTOs.Common;

namespace api.DTOs.Make
{
    public class MakeDTO : IdNameObjectDTO
    {
        public ICollection<IdNameObjectDTO> Models { get; set; }
        public MakeDTO(){
            Models = new Collection<IdNameObjectDTO>();
        }
    }
}