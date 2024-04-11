using JobNet.Exceptions;
using JobNet.Models.Core.Responses;

namespace JobNet.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception);
            if (exception.GetType() == typeof(BaseRequestException))
            {
                var requestException = (BaseRequestException)exception;
                context.Response.StatusCode = requestException.StatusCode;
                MessageResponse response = new()
                {
                    Message = requestException.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(exception.Message);
            }
        }
    }

}