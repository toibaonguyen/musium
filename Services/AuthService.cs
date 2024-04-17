using StackExchange.Redis;
using JobNet.DTOs;
using JobNet.Interfaces.Services;
using JobNet.Exceptions;
using JobNet.Utilities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using JobNet.Contants;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using JobNet.Models.Core.Common;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
namespace JobNet.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _usersService;
    private readonly IAdminService _adminsService;
    private readonly IConnectionMultiplexer _redis;
    private readonly IEmailSenderService _emailService;
    private readonly IUrlHelperFactory _urlHelperFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _secretKey;
    private readonly int _tokenValidityInMinutes;
    private readonly int _refreshTokenValidityInDays;
    private readonly string _validIssuerURL;
    private readonly string _validAudienceURL;


    public AuthService(IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IAdminService adminsService, IUserService usersService, IEmailSenderService emailService, IConnectionMultiplexer redis, IConfiguration configuration)
    {
        this._httpContextAccessor = httpContextAccessor;
        this._urlHelperFactory = urlHelperFactory;
        this._usersService = usersService;
        this._adminsService = adminsService;
        this._redis = redis;
        this._emailService = emailService;
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
            if (user.IsEmailConfirmed == false)
            {
                throw new UnauthorizedException("User's email is not confirmed!");
            }
            if (user.IsActive == false)
            {
                throw new UnauthorizedException("This user is inactive!");
            }
            if (PasswordUtil.VerifyPassword(password, user.Password, user.PasswordSalt))
            {
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email,user.Email),
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
                string key = $"Token:{UserRoles.User}:{user.Id}";
                await db.KeyDeleteAsync(key);
                await db.StringSetAsync(key, authModelJson);
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
            if (admin.IsActive == false)
            {
                throw new UnauthorizedException("this admin is inactive!");
            }
            if (PasswordUtil.VerifyPassword(password, admin.Password, admin.PasswordSalt))
            {
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email,admin.Email),
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
                string key = $"Token:{UserRoles.Admin}:{admin.Id}";
                await db.KeyDeleteAsync(key);
                await db.StringSetAsync(key, authModelJson);
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
                    throw new BadRequestException("Expired token, please login again!");
                }
                else
                {

                    switch (role)
                    {
                        case UserRoles.Admin:
                            {
                                var admin = await this._adminsService.GetAdminById(userId) ?? throw new BadRequestException("Invalid admin");
                                if (admin.IsActive == false)
                                {
                                    throw new UnauthorizedException("this admin is inactive!");
                                }
                                var authClaims = new List<Claim>
                                {
                                    new(ClaimTypes.Email,admin.Email),
                                    new(ClaimTypes.Name, admin.Name),
                                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new(ClaimTypes.Role,UserRoles.Admin)
                                };
                                var token = CreateToken(authClaims);
                                var newRefreshToken = GenerateRefreshToken();
                                fetchedAuthModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(this._refreshTokenValidityInDays);
                                fetchedAuthModel.UsedRefreshTokens.Add(fetchedAuthModel.RefreshToken);
                                fetchedAuthModel.RefreshToken = newRefreshToken;
                                await db.StringSetAsync(key, JsonSerializer.Serialize(fetchedAuthModel));
                                AuthenticationTokenDTO auth = new()
                                {
                                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                                    RefreshToken = newRefreshToken,
                                    Role = UserRoles.Admin
                                };
                                return auth;
                            }
                        case UserRoles.User:
                            {
                                var user = await this._usersService.GetUserById(userId) ?? throw new BadRequestException("Invalid user");
                                if (user.IsActive == false)
                                {
                                    throw new UnauthorizedException("this user is inactive!");
                                }
                                var authClaims = new List<Claim>
                                {
                                    new(ClaimTypes.Email,user.Email),
                                    new(ClaimTypes.Name, user.Name),
                                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new(ClaimTypes.Role,UserRoles.User)
                                };
                                var token = CreateToken(authClaims);
                                var newRefreshToken = GenerateRefreshToken();
                                fetchedAuthModel.RefreshToken = newRefreshToken;
                                fetchedAuthModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(this._refreshTokenValidityInDays);
                                fetchedAuthModel.UsedRefreshTokens.Add(fetchedAuthModel.RefreshToken);
                                await db.StringSetAsync(key, JsonSerializer.Serialize(fetchedAuthModel));
                                AuthenticationTokenDTO auth = new()
                                {
                                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                                    RefreshToken = newRefreshToken,
                                    Role = UserRoles.User
                                };
                                return auth;
                            }
                        default:
                            throw new BadRequestException("Invalid role");
                    }
                }
            }
            else
            {
                if (fetchedAuthModel.UsedRefreshTokens.Contains(refreshToken))
                {
                    await db.KeyDeleteAsync(key);
                    throw new BadRequestException("Expired token, please login again!");
                }
                throw new ForbiddenException("Don't have permission to request for new token");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task CreateNewAdmin(CreateAdminDTO DTO)
    {
        try
        {
            await _adminsService.CreateNewAdmin(DTO);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task RegisterUser(CreateUserDTO dto)
    {
        try
        {
            await _usersService.CreateNewActiveUser(dto, false);
            var user = await _usersService.GetUserByEmail(dto.Email) ?? throw new Exception("Something wrong when get this user from database");
            //Send confirm email here
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.Name, user.Name),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role,UserRoles.User)
            };
            var token = this.CreateToken(authClaims);
            if (_httpContextAccessor.HttpContext is null)
            {
                throw new Exception("HttpContext is null");
            }
            var urlHelper = this._urlHelperFactory.GetUrlHelper(new ActionContext(_httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor()));
            if (urlHelper is null)
            {
                await _usersService.DeleteUser(user.Id);
                throw new Exception("urlHelper is null");
            }

            string? verificationUrl = urlHelper.Action("VerifyEmail", "Authentication", new { userId = user.Id, token = new JwtSecurityTokenHandler().WriteToken(token) });
            if (verificationUrl is null)
            {
                await _usersService.DeleteUser(user.Id);
                throw new Exception("Can't generate verification link!");
            }
            verificationUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + verificationUrl;
            try
            {
                await _emailService.SendEmailVerificationAsync(dto.Email, verificationUrl);
            }
            catch (Exception)
            {
                await _usersService.DeleteUser(user.Id);
                throw;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ConfirmUser(int userId, string token)
    {
        try
        {
            var user = await _usersService.GetUserById(userId) ?? throw new BadRequestException("Invalid userId");
            if (user.IsEmailConfirmed)
            {
                throw new BadRequestException("This user is already confirmed!");
            }
            var isValid = this.ValidateToken(token, out JwtSecurityToken? jwt);

            Console.WriteLine("is valid:", isValid);
            if (isValid)
            {
                if (jwt is null)
                {
                    throw new Exception();
                }
                if (jwt.Claims.Single(x => x.Type == ClaimTypes.Email).Value == user.Email)
                {
                    await _usersService.ChangeEmailConfirmationStatus(userId, true);
                }
                else
                {
                    throw new UnauthorizedException("You don't have permission to confirm this user");
                }
            }
            else
            {
                throw new UnauthorizedException("You don't have permission to confirm this user");
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ResendVerificationEmail(string email)
    {
        try
        {
            var user = await _usersService.GetUserByEmail(email) ?? throw new Exception("Invalid user");
            if (user.IsEmailConfirmed)
            {
                throw new BadRequestException("This user is already confirmed!");
            }
            //Send confirm email here
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.Name, user.Name),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role,UserRoles.User)
            };
            var token = this.CreateToken(authClaims);
            if (_httpContextAccessor.HttpContext is null)
            {
                throw new Exception("HttpContext is null");
            }
            var urlHelper = this._urlHelperFactory.GetUrlHelper(new ActionContext(_httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor()));
            if (urlHelper is null)
            {
                await _usersService.DeleteUser(user.Id);
                throw new Exception("urlHelper is null");
            }
            string? verificationUrl = urlHelper.Action("VerifyEmail", "Authentication", new { userId = user.Id, token = new JwtSecurityTokenHandler().WriteToken(token) });
            if (verificationUrl is null)
            {
                await _usersService.DeleteUser(user.Id);
                throw new Exception("Can't generate verification link, please contact us!");
            }
            verificationUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + verificationUrl;
            await _emailService.SendEmailVerificationAsync(email, verificationUrl);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserPassword(int userId, string newPassword)
    {
        try
        {
            await _usersService.ChangeUserPassword(userId, newPassword);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeAdminPassword(int adminId, string newPassword)
    {
        try
        {
            await _adminsService.ChangeAdminPassword(adminId, newPassword);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task SendResetUserPasswordConfirmationEmail(string email)
    {
        try
        {
            var user = await _usersService.GetUserByEmail(email) ?? throw new BadRequestException("Invalid user");
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.Name, user.Name),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role,UserRoles.User)
            };
            var token = this.CreateToken(authClaims);
            if (_httpContextAccessor.HttpContext is null)
            {
                throw new Exception("HttpContext is null");
            }
            var urlHelper = this._urlHelperFactory.GetUrlHelper(new ActionContext(_httpContextAccessor.HttpContext, new RouteData(), new ActionDescriptor()));
            if (urlHelper is null)
            {
                await _usersService.DeleteUser(user.Id);
                throw new Exception("urlHelper is null");
            }
            string? confirmationUrl = urlHelper.Action("ConfirmResetPassword", "Authentication", new { userId = user.Id, token = new JwtSecurityTokenHandler().WriteToken(token) });
            if (confirmationUrl is null)
            {
                await _usersService.DeleteUser(user.Id);
                throw new Exception("Can't generate confirmationUrl link, please contact us!");
            }
            confirmationUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + confirmationUrl;

            await _emailService.SendResetPasswordConfirmationAsync(email, confirmationUrl);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ConfirmResetPassword(int userId, string token)
    {
        try
        {
            var user = await _usersService.GetUserById(userId) ?? throw new BadRequestException("Invalid userId");
            var isValid = this.ValidateToken(token, out JwtSecurityToken? jwt);
            if (isValid)
            {
                if (jwt is null)
                {
                    throw new Exception();
                }
                if (jwt.Claims.Single(x => x.Type == ClaimTypes.Email).Value == user.Email)
                {
                    string newPassword = GenerateRandomPassword();
                    await _usersService.ChangeUserPassword(userId, newPassword);
                    await _emailService.SendNewResetPasswordEmailAsync(user.Email, newPassword);
                }
                else
                {
                    throw new UnauthorizedException("You don't have permission to reset password of user");
                }
            }
            else
            {
                throw new UnauthorizedException("You don't have permission to reset password of user");
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    //-------------------------------------PRIVATE-------------------------------------//

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
    private bool ValidateToken(string token, out JwtSecurityToken? jwt)
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
            ValidAudience = _validAudienceURL,
            ValidIssuer = _validIssuerURL,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
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


    private static string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
        byte[] randomBytes = new byte[4];
#pragma warning disable SYSLIB0023 // Type or member is obsolete
        using (RNGCryptoServiceProvider rng = new())
        {
            rng.GetBytes(randomBytes);
        }
#pragma warning restore SYSLIB0023 // Type or member is obsolete
        Random random = new(BitConverter.ToInt32(randomBytes, 0));
        int passwordLength = random.Next(8, 15 + 1);
        char[] password = new char[passwordLength];

        for (int i = 0; i < passwordLength; i++)
        {
            password[i] = chars[random.Next(chars.Length)];
        }

        return new string(password);
    }
}