using Microsoft.EntityFrameworkCore;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produk> Produk { get; set; }
        public DbSet<Pesanan> Pesanan { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}