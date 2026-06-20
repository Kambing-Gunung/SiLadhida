using System.Net;
using System.Text.Json;
using SiLadhida.API.Common;
using SiLadhida.Core.Exceptions;

namespace SiLadhida.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "NotFound: {Message}", ex.Message);
            await HandleException(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business Error: {Message}", ex.Message);
            await HandleException(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception");
            await HandleException(context, HttpStatusCode.InternalServerError, "Terjadi kesalahan pada server");
        }
    }

    private static async Task HandleException(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = ApiResponse<object>.ErrorResponse(message);

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}