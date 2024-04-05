

namespace JobNet.DTOs;
public class AuthenticationTokenDTO
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required string Role { get; set; }
}
