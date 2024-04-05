using StackExchange.Redis;
using Microsoft.AspNetCore.Identity;
using JobNet.DTOs;
using JobNet.Models.Entities;
using JobNet.Extensions;
using JobNet.Interfaces;
using JobNet.Models.Core.Responses;
using JobNet.Interfaces.Services;
using JobNet.Exceptions;
using JobNet.Utilities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using JobNet.Contants;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using JobNet.Models.Core.Common;
using System.Text.Json;

namespace JobNet.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _usersService;
    private readonly IAdminService _adminsService;
    private readonly IConnectionMultiplexer _redis;
    private readonly string _secretKey;
    private readonly int _tokenValidityInMinutes;
    private readonly int _refreshTokenValidityInDays;
    private readonly string _validIssuerURL;
    private readonly string _validAudienceURL;


    public AuthService(IAdminService adminsService, IUserService usersService, IConnectionMultiplexer redis, IConfiguration configuration)
    {
        this._usersService = usersService;
        this._adminsService = adminsService;
        this._redis = redis;
        this._secretKey = configuration["JWTAuth:SecretKey"] ?? throw new Exception("Missing SecretKey");
        this._tokenValidityInMinutes = Int32.Parse(configuration["JWTAuth:TokenValidityInMinutes"] ?? throw new Exception("Missing TokenValidityInMinutes"));
        this._refreshTokenValidityInDays = Int32.Parse(configuration["JWTAuth:RefreshTokenValidityInDays"] ?? throw new Exception("Missing RefreshTokenValidityInDays"));
        this._validIssuerURL = configuration["JWTAuth:ValidIssuerURL"] ?? throw new Exception("Missing ValidIssuerURL");
        this._validAudienceURL = configuration["JWTAuth:ValidAudienceURL"] ?? throw new Exception("Missing ValidAudienceURL");
    }
    public async Task<AuthenticationTokenDTO> LoginAsUser(string email, string password)
    {
        try
        {
            var user = await _usersService.GetUserByEmail(email) ?? throw new BadRequestException("This user is not exist!");
            if (PasswordUtil.VerifyPassword(password, user.Password, user.PasswordSalt))
            {
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.Name),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role,UserRoles.User)
                };
                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();
                AuthModel authModel = new()
                {
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(this._refreshTokenValidityInDays),
                    UsedRefreshTokens = []
                };
                var db = _redis.GetDatabase();
                string authModelJson = JsonSerializer.Serialize(authModel);
                await db.KeyDeleteAsync($"Token:{UserRoles.User}:{user.Id}");
                await db.StringSetAsync($"Token:{UserRoles.User}:{user.Id}", authModelJson);
                AuthenticationTokenWithUserInfoDTO auth = new()
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = UserRoles.User
                };
                return auth;
            }
            else
            {
                throw new BadRequestException("Wrong password!");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<AuthenticationTokenDTO> LoginAsAdmin(string email, string password)
    {
        try
        {
            var admin = await _adminsService.GetAdminByEmail(email) ?? throw new BadRequestException("This admin is not exist!");
            if (PasswordUtil.VerifyPassword(password, admin.Password, admin.PasswordSalt))
            {
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, admin.Name),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role,UserRoles.Admin)
                };
                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();
                AuthModel authModel = new()
                {
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(this._refreshTokenValidityInDays),
                    UsedRefreshTokens = []
                };
                var db = _redis.GetDatabase();
                string authModelJson = JsonSerializer.Serialize(authModel);
                await db.KeyDeleteAsync($"Token:{UserRoles.Admin}:{admin.Id}");
                await db.StringSetAsync($"Token:{UserRoles.Admin}:{admin.Id}", authModelJson);
                AuthenticationTokenWithUserInfoDTO auth = new()
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    UserId = admin.Id,
                    Name = admin.Name,
                    Email = admin.Email,
                    Role = UserRoles.Admin
                };
                return auth;
            }
            else
            {
                throw new BadRequestException("Wrong password!");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<AuthenticationTokenDTO> RefreshTokens(int userId, string role, string refreshToken)
    {
        try
        {
            string key = $"Token:{role}:{userId}";
            var db = this._redis.GetDatabase();
            string? fetchedAuthModelJson = await db.StringGetAsync(key);
            if (fetchedAuthModelJson is null)
            {
                throw new ForbiddenException("This user have not login yet");
            }
            AuthModel? fetchedAuthModel = JsonSerializer.Deserialize<AuthModel>(fetchedAuthModelJson) ?? throw new Exception("There something wrong with storing auth token!");
            if (fetchedAuthModel.RefreshToken == refreshToken)
            {
                if (DateTime.Compare(fetchedAuthModel.RefreshTokenExpiryTime, DateTime.Now) < 0)
                {
                    await db.KeyDeleteAsync(key);
                    throw new BadRequestException("Invalid token, please login again!");
                }
                else
                {

                }
            }
            else
            {

            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var token = new JwtSecurityToken(
            issuer: _validIssuerURL,
            audience: _validAudienceURL,
            expires: DateTime.Now.AddMinutes(_tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        return token;
    }
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}