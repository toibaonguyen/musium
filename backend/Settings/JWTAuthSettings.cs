namespace JobNet.Settings;

public class JWTAuthSettings
{
    public string ValidAudienceURL { get; set; } = string.Empty;
    public string ValidIssuerURL { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int TokenValidityInMinutes { get; set; }
    public int RefreshTokenValidityInDays { get; set; }
    public int OuterTokenValidityInMinutes { get; set; }
}