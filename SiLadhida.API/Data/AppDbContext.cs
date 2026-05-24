using Microsoft.EntityFrameworkCore;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Produk> Produk => Set<Produk>();

    public DbSet<Pesanan> Pesanan => Set<Pesanan>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PRODUCT SEED
        modelBuilder.Entity<Produk>().HasData(
            new Produk
            {
                Id = 1,
                Nama = "Espresso",
                Harga = 18000,
                Stock = 50
            },
            new Produk
            {
                Id = 2,
                Nama = "Cappuccino",
                Harga = 25000,
                Stock = 40
            },
            new Produk
            {
                Id = 3,
                Nama = "Latte",
                Harga = 28000,
                Stock = 35
            },
            new Produk
            {
                Id = 4,
                Nama = "Americano",
                Harga = 22000,
                Stock = 60
            },
            new Produk
            {
                Id = 5,
                Nama = "Croissant",
                Harga = 15000,
                Stock = 25
            }
        );
    }
}