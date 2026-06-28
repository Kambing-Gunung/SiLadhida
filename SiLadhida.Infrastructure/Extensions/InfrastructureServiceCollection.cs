using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiLadhida.Infrastructure.Persistence;
using SiLadhida.Core.Interfaces;
using SiLadhida.Infrastructure.Repositories;

namespace SiLadhida.Infrastructure.Extensions;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("Default"),
                ServerVersion.AutoDetect(
                    configuration.GetConnectionString("Default")
                )
            ));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}