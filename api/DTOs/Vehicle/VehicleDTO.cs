using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using api.DTOs.Contact;

namespace api.DTOs.Vehicle
{
    public class VehicleDTO
    {
       // public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        public ContactDTO Contact { get; set; }
        public ICollection<int> Features { get; set; }
        public VehicleDTO()
        {
            Features = new Collection<int>();
        }
    }
}