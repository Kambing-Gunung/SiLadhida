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

// Use application middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseApplicationMiddleware(app.Environment);

app.MapControllers();

app.Run();
