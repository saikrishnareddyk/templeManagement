using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TempleManagementApi.Models;

namespace TempleManagementApi.Data.Configurations;

public class SevaConfiguration : IEntityTypeConfiguration<Seva>
{
    public void Configure(EntityTypeBuilder<Seva> builder)
    {
        builder.ToTable("Sevas");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SevaName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.Price)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(s => s.DurationMinutes)
            .IsRequired();

        builder.Property(s => s.CreatedDate)
            .IsRequired();

        builder.Property(s => s.UpdatedDate);
    }
}