namespace TempleManagementApi.Models
{
    public class Seva :BaseEntity
    {
        public string SevaName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int DurationMinutes { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public override string GetDisplayName()
        {
            return SevaName;
        }
    }
}
