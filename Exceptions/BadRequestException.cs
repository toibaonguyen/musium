
namespace JobNet.Exceptions;
public class BadRequestException : BaseRequestException
{
    public readonly int StatusCode = StatusCodes.Status400BadRequest;
    public BadRequestException(string? message) : base(message)
    {
    }
}