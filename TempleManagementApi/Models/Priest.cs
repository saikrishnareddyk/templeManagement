namespace TempleManagementApi.Models
{
    public class Priest : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public int ExperienceYears { get; set; }

        public override string GetDisplayName()
        {
            return FullName;
        }
    }
}
