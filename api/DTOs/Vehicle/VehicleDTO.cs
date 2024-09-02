using System.Collections.ObjectModel;
using api.DTOs.Common;
using api.DTOs.Contact;
using api.DTOs.Feature;
using api.DTOs.Make;

namespace api.DTOs.Vehicle
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public IdNameObjectDTO Model { get; set; }
        public IdNameObjectDTO Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactDTO Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<IdNameObjectDTO> Features { get; set; }
        public VehicleDTO()
        {
            Features = new Collection<IdNameObjectDTO>();
        }
    }
}