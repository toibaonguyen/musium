

using JobNet.Exceptions;

public class UnauthorizedException : BaseRequestException
{
    public UnauthorizedException(string? message) : base(message, StatusCodes.Status401Unauthorized)
    {
    }
}