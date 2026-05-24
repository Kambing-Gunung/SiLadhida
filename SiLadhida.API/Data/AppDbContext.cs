using Microsoft.EntityFrameworkCore;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Data;

/// <summary>
/// Entity Framework Core database context for the application
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the products table
    /// </summary>
    public DbSet<Produk> Produk { get; set; }

    /// <summary>
    /// Gets or sets the orders table
    /// </summary>
    public DbSet<Pesanan> Pesanan { get; set; }

    /// <summary>
    /// Gets or sets the order items table
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}