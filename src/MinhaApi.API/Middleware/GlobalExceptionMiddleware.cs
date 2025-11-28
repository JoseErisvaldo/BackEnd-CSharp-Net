using System.Net;
using System.Text.Json;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex, _env.IsDevelopment());
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex, bool isDevelopment)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorId = Guid.NewGuid().ToString("N");
        var response = new
        {
            message = "Ocorreu um erro inesperado.",
            errorId,
            details = isDevelopment ? ex.Message : null
        };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}