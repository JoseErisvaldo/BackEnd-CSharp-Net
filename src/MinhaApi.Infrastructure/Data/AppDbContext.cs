using Microsoft.EntityFrameworkCore;
using MinhaApi.Entities;

namespace MinhaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Establishments> Establishments { get; set; } = null!;
    public DbSet<Restaurants> Restaurants { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurants>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.Establishment)
                  .WithMany(e => e.Restaurants)
                  .HasForeignKey(r => r.EstablishmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
