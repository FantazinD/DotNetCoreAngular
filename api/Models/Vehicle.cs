using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; } = string.Empty;

        [StringLength(255)]
        public string ContactEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; }
        public ICollection<VehicleFeature> Features { get; set; }
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
        }
    }
}