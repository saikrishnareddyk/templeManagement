using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TempleManagementApi.Models;

namespace TempleManagementApi.Data.Configurations;

public class DevoteeConfiguration : IEntityTypeConfiguration<Devotee>
{
    public void Configure(EntityTypeBuilder<Devotee> builder)
    {
        builder.ToTable("Devotees");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.MobileNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(d => d.Email)
            .HasMaxLength(100);

        builder.Property(d => d.Address)
            .HasMaxLength(250);

        builder.Property(d => d.CreatedDate)
            .IsRequired();

        builder.Property(d => d.UpdatedDate);
    }
}