using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TempleManagementApi.Models;

namespace TempleManagementApi.Data.Configurations;

public class PriestConfiguration : IEntityTypeConfiguration<Priest>
{
    public void Configure(EntityTypeBuilder<Priest> builder)
    {
        builder.ToTable("Priests");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Specialization)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.ExperienceYears)
            .IsRequired();

        builder.Property(p => p.CreatedDate)
            .IsRequired();

        builder.Property(p => p.UpdatedDate);
    }
}