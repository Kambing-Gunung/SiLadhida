using Microsoft.EntityFrameworkCore;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Product => Set<Product>();

    public DbSet<Order> Order => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    
}