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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PRODUCT SEED
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Nama = "Espresso",
                Harga = 18000,
                Stock = 50
            },
            new Product
            {
                Id = 2,
                Nama = "Cappuccino",
                Harga = 25000,
                Stock = 40
            },
            new Product
            {
                Id = 3,
                Nama = "Latte",
                Harga = 28000,
                Stock = 35
            },
            new Product
            {
                Id = 4,
                Nama = "Americano",
                Harga = 22000,
                Stock = 60
            },
            new Product
            {
                Id = 5,
                Nama = "Croissant",
                Harga = 15000,
                Stock = 25
            }
        );
    }
}