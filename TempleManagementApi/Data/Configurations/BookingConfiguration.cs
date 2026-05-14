using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TempleManagementApi.Models;

namespace TempleManagementApi.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingDate)
            .IsRequired();

        builder.Property(b => b.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.CreatedDate)
            .IsRequired();

        builder.Property(b => b.UpdatedDate);

        builder.HasOne(b => b.Devotee)
            .WithMany(d => d.Bookings)
            .HasForeignKey(b => b.DevoteeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Seva)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.SevaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}