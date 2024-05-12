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
using JobNet.Settings;
using Microsoft.Extensions.Options;
using JobNet.Models.Core.Responses;
using JobNet.Extensions;
namespace JobNet.Services;

public class AuthService : IAuthService
{
    private readonly string USER_EMAIL_HAS_NOT_BEEN_CONFIRMED_YET = "User's email is not confirmed!";
    private readonly string USER_IS_INACTIVE = "User is inactive!";
    private readonly string USER_HAS_NOT_LOGIN_YET = "User has not login yet!";
    private readonly string EXPIRED_TOKEN = "Expired token, please login again!";
    private readonly string WRONG_EMAIL_OR_PASSWORD = "Wrong email or password!";
    private readonly string USER_IS_INVALID = "User is invalid!";
    private readonly string ROLE_IS_INVALID = "this role is invalid!";
    private readonly string MISSING_REGISTRATION_TOKEN = "Missing registration token!";

    private readonly int MIN_PASSWORD_LENGTH = 8, MAX_PASSWORD_LENGTH = 15;
    private readonly JWTAuthSettings _jwtAuthSettings;
    private readonly IUserService _usersService;
    private readonly IAdminService _adminsService;
    private readonly ICloudMessageRegistrationTokenService _cloudMessageRegistrationTokenService;
    private readonly IConnectionMultiplexer _redis;
    private readonly IEmailSenderService _emailService;
    private readonly IUrlHelperFactory _urlHelperFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private string GetRedisStoredKey(string userRole, int userId)
    {
        return $"Token:{userRole}:{userId}";
    }
    public AuthService(IOptions<JWTAuthSettings> jwtAuthOptions, IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory, IAdminService adminsService, IUserService usersService, IEmailSenderService emailService, ICloudMessageRegistrationTokenService cloudMessageRegistrationTokenService, IConnectionMultiplexer redis)
    {
        _cloudMessageRegistrationTokenService = cloudMessageRegistrationTokenService;
        _httpContextAccessor = httpContextAccessor;
        _urlHelperFactory = urlHelperFactory;
        _usersService = usersService;
        _adminsService = adminsService;
        _redis = redis;
        _emailService = emailService;
        _jwtAuthSettings = jwtAuthOptions.Value;
    }
    public async Task<AuthenticationResponse> LoginAsUser(string email, string password)
    {
        try
        {
            var user = await _usersService.GetUserByEmail(email) ?? throw new BadRequestException(WRONG_EMAIL_OR_PASSWORD);
            if (user.IsEmailConfirmed == false)
            {
                throw new UnauthorizedException(USER_EMAIL_HAS_NOT_BEEN_CONFIRMED_YET);
            }
            if (user.IsActive == false)
            {
                throw new UnauthorizedException(USER_IS_INACTIVE);
            }
            if (PasswordUtil.VerifyPassword(password, user.Password, user.PasswordSalt))
            {
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email,user.Email),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role,UserRoles.User)
                };
                var token = TokenUtil.CreateToken(authClaims, _jwtAuthSettings.SecretKey, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.TokenValidityInMinutes);
                var refreshToken = TokenUtil.GenerateRefreshToken();
                AuthModel authModel = new()
                {
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtAuthSettings.RefreshTokenValidityInDays),
                    UsedRefreshTokens = []
                };
                var db = _redis.GetDatabase();
                string authModelJson = JsonSerializer.Serialize(authModel);
                string key = GetRedisStoredKey(UserRoles.User, user.Id);
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
                return new AuthenticationResponse
                {
                    Data = auth,
                    User = user.ToUserIdentityDTO()
                };
            }
            else
            {
                throw new BadRequestException(WRONG_EMAIL_OR_PASSWORD);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<AuthenticationResponse> LoginAsAdmin(string email, string password)
    {
        try
        {
            var admin = await _adminsService.GetAdminByEmail(email) ?? throw new BadRequestException(WRONG_EMAIL_OR_PASSWORD);
            if (admin.IsActive == false)
            {
                throw new UnauthorizedException(USER_IS_INACTIVE);
            }
            if (PasswordUtil.VerifyPassword(password, admin.Password, admin.PasswordSalt))
            {
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Email,admin.Email),
                    new(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Role,UserRoles.Admin)
                };
                var token = TokenUtil.CreateToken(authClaims, _jwtAuthSettings.SecretKey, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.TokenValidityInMinutes);
                var refreshToken = TokenUtil.GenerateRefreshToken();
                AuthModel authModel = new()
                {
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtAuthSettings.RefreshTokenValidityInDays),
                    UsedRefreshTokens = []
                };
                var db = _redis.GetDatabase();
                string authModelJson = JsonSerializer.Serialize(authModel);
                string key = GetRedisStoredKey(UserRoles.Admin, admin.Id);
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
                return new AuthenticationResponse
                {
                    Data = auth,
                    User = admin.ToUserIdentityDTO()
                };
            }
            else
            {
                throw new BadRequestException(WRONG_EMAIL_OR_PASSWORD);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<AuthenticationResponse> RefreshTokens(int userId, string role, string refreshToken)
    {
        try
        {
            string key = GetRedisStoredKey(role, userId);
            var db = this._redis.GetDatabase();
            string? fetchedAuthModelJson = await db.StringGetAsync(key);
            if (fetchedAuthModelJson is null)
            {
                throw new ForbiddenException(USER_HAS_NOT_LOGIN_YET);
            }
            AuthModel? fetchedAuthModel = JsonSerializer.Deserialize<AuthModel>(fetchedAuthModelJson) ?? throw new Exception();
            if (fetchedAuthModel.RefreshToken == refreshToken)
            {
                if (DateTime.Compare(fetchedAuthModel.RefreshTokenExpiryTime, DateTime.Now) < 0)
                {
                    await db.KeyDeleteAsync(key);
                    throw new BadRequestException(EXPIRED_TOKEN);
                }
                else
                {

                    switch (role)
                    {
                        case UserRoles.Admin:
                            {
                                var admin = await this._adminsService.GetAdminById(userId) ?? throw new BadRequestException(WRONG_EMAIL_OR_PASSWORD);
                                if (admin.IsActive == false)
                                {
                                    throw new UnauthorizedException(USER_IS_INACTIVE);
                                }
                                var authClaims = new List<Claim>
                                {
                                    new(ClaimTypes.Email,admin.Email),
                                    new(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new(ClaimTypes.Role,UserRoles.Admin)
                                };
                                var token = TokenUtil.CreateToken(authClaims, _jwtAuthSettings.SecretKey, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.TokenValidityInMinutes);
                                var newRefreshToken = TokenUtil.GenerateRefreshToken();
                                fetchedAuthModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtAuthSettings.RefreshTokenValidityInDays);
                                fetchedAuthModel.UsedRefreshTokens.Add(fetchedAuthModel.RefreshToken);
                                fetchedAuthModel.RefreshToken = newRefreshToken;
                                await db.StringSetAsync(key, JsonSerializer.Serialize(fetchedAuthModel));
                                AuthenticationTokenDTO auth = new()
                                {
                                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                                    RefreshToken = newRefreshToken,
                                    Role = UserRoles.Admin
                                };
                                return new AuthenticationResponse
                                {
                                    Data = auth,
                                    User = admin.ToUserIdentityDTO()
                                };
                            }
                        case UserRoles.User:
                            {
                                var user = await this._usersService.GetUserById(userId) ?? throw new BadRequestException(USER_IS_INVALID);
                                if (user.IsActive == false)
                                {
                                    throw new UnauthorizedException(USER_IS_INACTIVE);
                                }
                                var authClaims = new List<Claim>
                                {
                                    new(ClaimTypes.Email,user.Email),
                                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new(ClaimTypes.Role,UserRoles.User)
                                };
                                var token = TokenUtil.CreateToken(authClaims, _jwtAuthSettings.SecretKey, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.TokenValidityInMinutes);
                                var newRefreshToken = TokenUtil.GenerateRefreshToken();
                                fetchedAuthModel.RefreshToken = newRefreshToken;
                                fetchedAuthModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtAuthSettings.RefreshTokenValidityInDays);
                                fetchedAuthModel.UsedRefreshTokens.Add(fetchedAuthModel.RefreshToken);
                                await db.StringSetAsync(key, JsonSerializer.Serialize(fetchedAuthModel));
                                AuthenticationTokenDTO auth = new()
                                {
                                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                                    RefreshToken = newRefreshToken,
                                    Role = UserRoles.User
                                };
                                return new AuthenticationResponse
                                {
                                    Data = auth,
                                    User = user.ToUserIdentityDTO()
                                };
                            }
                        default:
                            throw new BadRequestException(ROLE_IS_INVALID);
                    }
                }
            }
            else
            {
                if (fetchedAuthModel.UsedRefreshTokens.Contains(refreshToken))
                {
                    await db.KeyDeleteAsync(key);
                    throw new BadRequestException(EXPIRED_TOKEN);
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
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role,UserRoles.User)
            };
            var token = TokenUtil.CreateToken(authClaims, _jwtAuthSettings.SecretKey, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.OuterTokenValidityInMinutes);
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
            var isValid = TokenUtil.ValidateToken(token, out JwtSecurityToken? jwt, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.SecretKey);

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
            var user = await _usersService.GetUserByEmail(email) ?? throw new Exception(WRONG_EMAIL_OR_PASSWORD);
            if (user.IsEmailConfirmed)
            {
                throw new BadRequestException("This user is already confirmed!");
            }
            //Send confirm email here
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role,UserRoles.User)
            };
            var token = TokenUtil.CreateToken(authClaims, _jwtAuthSettings.SecretKey, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.OuterTokenValidityInMinutes);
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
            var user = await _usersService.GetUserByEmail(email) ?? throw new BadRequestException(WRONG_EMAIL_OR_PASSWORD);
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role,UserRoles.User)
            };
            var token = TokenUtil.CreateToken(authClaims, _jwtAuthSettings.SecretKey, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.OuterTokenValidityInMinutes);
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
            var user = await _usersService.GetUserById(userId) ?? throw new BadRequestException(USER_IS_INVALID);
            var isValid = TokenUtil.ValidateToken(token, out JwtSecurityToken? jwt, _jwtAuthSettings.ValidIssuerURL, _jwtAuthSettings.ValidAudienceURL, _jwtAuthSettings.SecretKey);
            if (isValid)
            {
                if (jwt is null)
                {
                    throw new Exception();
                }
                if (jwt.Claims.Single(x => x.Type == ClaimTypes.Email).Value == user.Email)
                {
                    string newPassword = PasswordUtil.GenerateRandomPassword(this.MIN_PASSWORD_LENGTH, this.MAX_PASSWORD_LENGTH);
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
    public async Task Logout(string userRole, int userId, string? notificationRegistrationToken)
    {
        try
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(GetRedisStoredKey(userRole, userId));
            if (userRole == UserRoles.User)
            {
                if (notificationRegistrationToken is null)
                {
                    throw new BadRequestException(MISSING_REGISTRATION_TOKEN);
                }
                await _cloudMessageRegistrationTokenService.DeleteTokenAsync(userId, notificationRegistrationToken);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}