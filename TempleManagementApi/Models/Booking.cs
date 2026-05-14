namespace TempleManagementApi.Models
{
    public class Booking : BaseEntity
    {
        public int DevoteeId { get; set; }

        public Devotee? Devotee { get; set; }

        public int SevaId { get; set; }

        public Seva? Seva { get; set; }

        public DateTime BookingDate { get; set; }

        public string Status { get; set; } = "Pending";

        public override string GetDisplayName()
        {
            return $"Booking Id: {Id}, Status: {Status}";
        }
    }
}
