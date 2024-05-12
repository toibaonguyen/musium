
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JobNet.Utilities;

public static class TokenUtil
{
    public static JwtSecurityToken CreateToken(List<Claim> authClaims, string secretKey, string? validIssuer, string? validAudience, int tokenLifetimeInMinutes)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var token = new JwtSecurityToken(
            issuer: validIssuer,
            audience: validAudience,
            expires: DateTime.Now.AddMinutes(tokenLifetimeInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        return token;
    }
    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    public static bool ValidateToken(string token, out JwtSecurityToken? jwt, string? validIssuer, string? validAudience, string secretKey)
    {
        var validationParameters = new TokenValidationParameters
        {
            //Co gi do sai sai o day
            ValidateIssuer = false,
            // Ngay phia tren
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = validAudience,
            ValidIssuer = validIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        };
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            jwt = (JwtSecurityToken)validatedToken;
            return true;
        }
        catch (SecurityTokenValidationException ex)
        {
            Console.Error.WriteLine(ex.Message);
            jwt = null;
            return false;
        }
    }
}