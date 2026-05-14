using Microsoft.EntityFrameworkCore;
using TempleManagementApi.Models;

namespace TempleManagementApi.Data;

public class TempleDbContext : DbContext
{
    public TempleDbContext(DbContextOptions<TempleDbContext> options)
        : base(options)
    {
    }

    public DbSet<Devotee> Devotees { get; set; }
    public DbSet<Priest> Priests { get; set; }
    public DbSet<Seva> Sevas { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Donation> Donations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TempleDbContext).Assembly);
    }
}