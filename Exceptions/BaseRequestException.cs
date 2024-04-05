
namespace JobNet.Exceptions;
public abstract class BaseRequestException : Exception
{
    public BaseRequestException(string? message) : base(message)
    {
    }
}