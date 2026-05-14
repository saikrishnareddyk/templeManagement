namespace TempleManagementApi.Models
{
    public class Donation : BaseEntity
    {
        public int DevoteeId { get; set; }
        public Devotee? Devotee { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMode { get; set; } = string.Empty;

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;

        public override string GetDisplayName()
        {
            return $"Donation Amount: {Amount}";
        }


    }
}
