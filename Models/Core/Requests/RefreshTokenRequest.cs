using JobNet.Models.Core.Requests;

public class RefreshTokenRequest : BaseRequest
{
    public required string RefreshToken { get; set; }
}