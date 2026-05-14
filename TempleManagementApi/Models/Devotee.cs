namespace TempleManagementApi.Models
{
    public class Devotee :BaseEntity
    {
        public string FullName { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();

        public override string GetDisplayName()
        {
            return FullName;
        }
    }
}
