
namespace JobNet.Middlewares;

public class CheckExistNotificationRegistrationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CheckExistNotificationRegistrationMiddleware> _logger;

    public CheckExistNotificationRegistrationMiddleware(ILogger<CheckExistNotificationRegistrationMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Kiểm tra xem header có tồn tại không
        if (context.Request.Headers.ContainsKey("YourHeaderName"))
        {
            // Nếu header tồn tại, chuyển hành động tiếp theo trong pipeline middleware
            await _next(context);
        }
        else
        {
            // Nếu header không tồn tại, trả về lỗi hoặc xử lý tùy ý
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Header is missing");
        }
    }
}