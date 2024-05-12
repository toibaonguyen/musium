using System.IdentityModel.Tokens.Jwt;

namespace JobNet.Models.Core.Common;

public class AuthModel : BaseCommonModel
{
    public required string RefreshToken { get; set; }
    public required DateTime RefreshTokenExpiryTime { get; set; }
    public required List<string> UsedRefreshTokens { get; set; }

}