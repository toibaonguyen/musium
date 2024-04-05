


namespace JobNet.DTOs;

public class AuthenticationTokenWithUserInfoDTO : AuthenticationTokenDTO
{
    public required int UserId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}