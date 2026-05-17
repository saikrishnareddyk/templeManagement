using System.ComponentModel.DataAnnotations;

namespace TempleCsrApi.Models
{
    public class Devotee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(50)]
        public string? Gothram { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}