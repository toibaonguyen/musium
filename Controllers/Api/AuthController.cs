using JobNet.Contants;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Requests;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly string CREATE_ACCOUNT_SUCCESSFULLY = "Create account successfully, please confirm email!";
    private readonly string CHECK_EMAIL = "Please check your email!";
    private readonly string LOGOUT_SUCCESSFULLY = "Logout successfully!";
    private readonly string INVALID_TOKEN = "Invalid token!";
    private readonly string INVALID_USER = "Invalid user!";
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IAdminService _adminService;
    private readonly ILogger<AuthController> _logger;
    private readonly IUrlHelperFactory _urlHelperFactory;
    public AuthController(ILogger<AuthController> logger, IAuthService authService, IUrlHelperFactory urlHelperFactory, IUserService userService, IAdminService adminService)
    {
        _userService = userService;
        _authService = authService;
        _adminService = adminService;
        _logger = logger;
        _urlHelperFactory = urlHelperFactory;
    }

    [AllowAnonymous]
    [HttpPost("users")]
    public async Task<ActionResult<BaseResponse>> RegisterUser([FromBody] RegisterUserRequest requestBody)
    {
        try
        {
            IUrlHelper urlHelper = Url;
            string requestScheme = Request.Scheme;
            await _authService.RegisterUser(requestBody.Data);

            var res = new MessageResponse
            {
                Message = CREATE_ACCOUNT_SUCCESSFULLY
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
    [AllowAnonymous]
    [HttpPost("users/login")]
    public async Task<ActionResult<BaseResponse>> LoginAsUser([FromBody] LoginRequest account)
    {
        try
        {
            var auth = await _authService.LoginAsUser(account.Email, account.Password);
            return Ok(auth);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [AllowAnonymous]
    [HttpPost("users/re-verify-email")]
    public async Task<ActionResult<BaseResponse>> ReVerifyEmailUser([FromBody] LoginRequest account)
    {
        try
        {
            await _authService.ResendVerificationEmail(account.Email);
            var Response = new MessageResponse
            {
                Message = CHECK_EMAIL
            };
            return Ok(Response);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost("admins/login")]
    public async Task<ActionResult<BaseResponse>> LoginAsAdmin([FromBody] LoginRequest account)
    {
        try
        {
            var auth = await _authService.LoginAsAdmin(account.Email, account.Password);
            return Ok(auth);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost("users/{userId}/refresh-tokens")]
    public async Task<ActionResult<BaseResponse>> RefreshUserToken(int userId, [FromBody] RefreshTokenRequest refreshToken)
    {
        try
        {
            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return BadRequest(
                    new MessageResponse
                    {
                        Message = INVALID_USER
                    });
            }

            var auth = await _authService.RefreshTokens(userId, UserRoles.User, refreshToken.RefreshToken) ?? throw new Exception();

            return Ok(auth);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [AllowAnonymous]
    [HttpPost("admins/{userId}/refresh-tokens")]
    public async Task<ActionResult<BaseResponse>> RefreshAdminToken(int userId, [FromBody] RefreshTokenRequest refreshToken)
    {
        try
        {
            var admin = await _adminService.GetAdminById(userId);
            if (admin == null)
            {
                return BadRequest(new MessageResponse
                {
                    Message = INVALID_USER
                });
            }
            AuthenticationResponse auth = await _authService.RefreshTokens(userId, UserRoles.Admin, refreshToken.RefreshToken) ?? throw new Exception();
            return Ok(auth);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost("admins")]
    public async Task<ActionResult<BaseResponse>> CreateNewAdmin([FromBody] CreateAdminRequest request)
    {
        try
        {
            await _authService.CreateNewAdmin(request.Data);
            return Created();
        }
        catch (Exception)
        {
            throw;
        }
    }
    [AllowAnonymous]
    [HttpPost("users/reset-password")]
    public async Task<ActionResult<BaseResponse>> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            await _authService.SendResetUserPasswordConfirmationEmail(request.Email);
            return Ok(new MessageResponse
            {
                Message = CHECK_EMAIL
            });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost("users/logout")]
    public async Task<ActionResult<BaseResponse>> LogoutUser()
    {
        try
        {
            var userId = HttpContext.User.FindFirst("userId")?.Value;
            if (userId is null)
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = INVALID_TOKEN
                    }
                );
            }
            //sua sau
            await _authService.Logout(UserRoles.User, int.Parse(userId), null);
            return Ok(new MessageResponse
            {
                Message = LOGOUT_SUCCESSFULLY
            });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost("admins/logout")]
    public async Task<ActionResult<BaseResponse>> LogoutAdmin()
    {
        try
        {
            var userId = HttpContext.User.FindFirst("userId")?.Value;
            if (userId is null)
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = INVALID_TOKEN
                    }
                );
            }
            await _authService.Logout(UserRoles.Admin, int.Parse(userId), null);
            return Ok(new MessageResponse
            {
                Message = LOGOUT_SUCCESSFULLY
            });
        }
        catch (Exception)
        {
            throw;
        }
    }
}