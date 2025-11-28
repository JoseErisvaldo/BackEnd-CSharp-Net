using Microsoft.EntityFrameworkCore;

using MinhaApi.Domain.Entities;

namespace MinhaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Establishment> Establishments { get; set; } = null!;


}