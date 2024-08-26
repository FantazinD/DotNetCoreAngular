using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Contact
{
    public class ContactDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Phone { get; set; } = string.Empty;
    }
}