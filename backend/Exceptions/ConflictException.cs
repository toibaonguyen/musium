namespace JobNet.Exceptions;

public class ConflictException : BaseRequestException
{
    public ConflictException(string? message) : base(message, StatusCodes.Status409Conflict)
    {

    }
}