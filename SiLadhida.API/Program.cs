using SiLadhida.API.Extensions;
using SiLadhida.Infrastructure.Extensions;
using SiLadhida.Infrastructure.Persistence;
using SiLadhida.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationControllers()
    .AddApplicationValidation()
    .AddApplicationServices()
    .AddApplicationMapping()
    .AddApplicationAuthentication(builder.Configuration)
    .AddApplicationSwagger()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("Incoming Request: {Method} {Path}",
        context.Request.Method,
        context.Request.Path);

    await next();
});

app.UseMiddleware<ExceptionMiddleware>();
app.UseApplicationMiddleware(app.Environment);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await ProductSeeder.SeedAsync(db);
    await UserSeeder.SeedAsync(db);
}

app.Run();