namespace TempleCsrApi.Models
{
    public class Devotee
    {
        public int Id { get; set; }
        
        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Gothram { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }



    }
}
