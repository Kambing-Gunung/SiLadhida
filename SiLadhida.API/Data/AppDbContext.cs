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
                Nama = "Bolu Coklat",
                Harga = 18000,
                Stock = 50
            },
            new Product
            {
                Id = 2,
                Nama = "Bolu Strawberry",
                Harga = 25000,
                Stock = 40
            },
            new Product
            {
                Id = 3,
                Nama = "Bolu Pandan",
                Harga = 28000,
                Stock = 35
            },
            new Product
            {
                Id = 4,
                Nama = "Bolu Keju",
                Harga = 22000,
                Stock = 60
            },
            new Product
            {
                Id = 5,
                Nama = "Bolu Coklat Keju",
                Harga = 15000,
                Stock = 25
            },
            new Product
            {
                Id = 6,
                Nama = "Bolu Keju Pandan",
                Harga = 15000,
                Stock = 25
            },
            new Product
            {
                Id = 7,
                Nama = "Bolu Coklat Strawberry",
                Harga = 15000,
                Stock = 25
            },
            new Product
            {
                Id = 8,
                Nama = "Nastar",
                Harga = 15000,
                Stock = 25
            },
            new Product
            {
                Id = 9,
                Nama = "Kastengel",
                Harga = 15000,
                Stock = 25
            },
            new Product
            {
                Id = 10,
                Nama = "Kue Sagu Keju",
                Harga = 15000,
                Stock = 25
            },
            new Product
            {
                Id = 11,
                Nama = "Kue Rambutan Coklat",
                Harga = 15000,
                Stock = 25
            }
        );
    }
}