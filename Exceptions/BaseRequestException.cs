
namespace JobNet.Exceptions;
public abstract class BaseRequestException : Exception
{
    public readonly int StatusCode;
    public BaseRequestException(string? message, int statusCode) : base(message)
    {
        this.StatusCode = statusCode;
    }
}