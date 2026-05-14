using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TempleManagementApi.Models;

namespace TempleManagementApi.Data.Configurations;

public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.ToTable("Donations");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(d => d.PaymentMode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.DonationDate)
            .IsRequired();

        builder.Property(d => d.CreatedDate)
            .IsRequired();

        builder.Property(d => d.UpdatedDate);

        builder.HasOne(d => d.Devotee)
            .WithMany(devotee => devotee.Donations)
            .HasForeignKey(d => d.DevoteeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}