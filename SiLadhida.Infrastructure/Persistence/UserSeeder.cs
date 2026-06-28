using SiLadhida.Core.Entities;

namespace SiLadhida.Infrastructure.Persistence;

public static class UserSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Users.Any())
            return;

        var users = new List<User>
        {
            User.Create("admin", BCrypt.Net.BCrypt.HashPassword("admin123"), "Admin"),
            User.Create("kasir", BCrypt.Net.BCrypt.HashPassword("kasir123"), "Kasir")
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}