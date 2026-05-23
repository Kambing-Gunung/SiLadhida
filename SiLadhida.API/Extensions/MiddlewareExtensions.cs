using SiLadhida.API.Middleware;

namespace SiLadhida.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApplicationMiddleware(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
