
namespace JobNet.Exceptions;
public class BadRequestException : BaseRequestException
{
    public BadRequestException(string? message) : base(message, StatusCodes.Status400BadRequest)
    {
    }
}