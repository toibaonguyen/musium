
namespace JobNet.Exceptions;
public class ForbiddenException : BaseRequestException
{
    public readonly int StatusCode = StatusCodes.Status403Forbidden;
    public ForbiddenException(string? message) : base(message)
    {
    }
}