using SiLadhida.API.Data;
using SiLadhida.API.Extensions;
using SiLadhida.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add application services
builder.Services
    .AddApplicationControllers()
    .AddApplicationValidation()
    .AddApplicationDatabase(builder.Configuration)
    .AddApplicationServices()
    .AddApplicationMapping()
    .AddApplicationAuthentication(builder.Configuration)
    .AddApplicationSwagger();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db =
        scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

    await ProductSeeder.SeedAsync(db);
}

// Use application middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseApplicationMiddleware(app.Environment);

app.MapControllers();

app.Run();
