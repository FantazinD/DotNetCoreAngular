using System.Collections.ObjectModel;
using api.DTOs.Contact;
using api.DTOs.Feature;
using api.DTOs.Make;
using api.DTOs.Model;

namespace api.DTOs.Vehicle
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public ModelDTO Model { get; set; }
        public MakeDTO Make { get; set; }
        public bool IsRegistered { get; set; }
        public ContactDTO Contact { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<FeatureDTO> Features { get; set; }
        public VehicleDTO()
        {
            Features = new Collection<FeatureDTO>();
        }
    }
}