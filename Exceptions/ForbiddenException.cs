
namespace JobNet.Exceptions;
public class ForbiddenException : BaseRequestException
{
    public ForbiddenException(string? message) : base(message, StatusCodes.Status403Forbidden)
    {
    }
}