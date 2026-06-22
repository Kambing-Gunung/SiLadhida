using SiLadhida.API.Auth;
using SiLadhida.Application.Services;
using SiLadhida.Application.Interfaces;
using SiLadhida.API.Mappings;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Text.Json.Serialization;

namespace SiLadhida.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }

    public static IServiceCollection AddApplicationControllers(
        this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return services;
    }

    public static IServiceCollection AddApplicationValidation(
        this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Program>();

        services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new
                {
                    success = false,
                    message = "Validation failed",
                    errors
                };

                return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(response);
            };
        });

        return services;
    }

    public static IServiceCollection AddApplicationMapping(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }
}