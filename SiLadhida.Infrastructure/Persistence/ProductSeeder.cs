using Microsoft.EntityFrameworkCore;
using SiLadhida.Core.Entities;

namespace SiLadhida.Infrastructure.Persistence;

public static class ProductSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Products.AnyAsync())
            return;

        var products = new List<Product>
        {
            Product.Create("Bolu Coklat", 18000, 50),
            Product.Create("Bolu Strawberry", 25000, 40),
            Product.Create("Bolu Pandan", 28000, 35),
            Product.Create("Bolu Keju", 22000, 60),
            Product.Create("Bolu Coklat Keju", 15000, 25),
            Product.Create("Bolu Keju Pandan", 15000, 25),
            Product.Create("Bolu Coklat Strawberry", 22000, 25),
            Product.Create("Nastar", 22000, 25),
            Product.Create("Kastengel", 22000, 25),
            Product.Create("Kue Sagu Keju", 22000, 25),
            Product.Create("Kue Rambutan Coklat", 22000, 25)
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}