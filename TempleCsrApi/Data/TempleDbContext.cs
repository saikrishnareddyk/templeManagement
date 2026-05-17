using Microsoft.EntityFrameworkCore;
using TempleCsrApi.Models;

namespace TempleCsrApi.Data
{
    public class TempleDbContext : DbContext
    {
        public TempleDbContext(DbContextOptions<TempleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Devotee> Devotees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Devotee>(entity =>
            {
                entity.ToTable("Devotees");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(d => d.Email)
                    .HasMaxLength(100);

                entity.Property(d => d.Gothram)
                    .HasMaxLength(50);
            });
        }
    }
}