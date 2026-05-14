namespace TempleManagementApi.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        public virtual string GetDisplayName()
        {
            return $"Record Id: {Id}";
        }


    }
}
