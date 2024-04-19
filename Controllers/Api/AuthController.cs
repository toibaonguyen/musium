using JobNet.Contants;
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
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly IUrlHelperFactory _urlHelperFactory;
    public AuthController(ILogger<AuthController> logger, IAuthService authService, IUrlHelperFactory urlHelperFactory)
    {
        this._authService = authService;
        this._logger = logger;
        this._urlHelperFactory = urlHelperFactory;
    }

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
                Message = "Create account successfully, please confirm email!"
            };
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
    [HttpPost("users/login")]
    public async Task<ActionResult<BaseResponse>> LoginAsUser([FromBody] LoginRequest account)
    {
        try
        {
            var auth = await _authService.LoginAsUser(account.Email, account.Password);
            if (auth == null)
            {
                throw new Exception("Something wrong!");
            }
            var Response = new AuthenticationResponse
            {
                Data = auth
            };
            return Ok(Response);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost("users/re-verify-email")]
    public async Task<ActionResult<BaseResponse>> ReVerifyEmailUser([FromBody] LoginRequest account)
    {
        try
        {
            await _authService.ResendVerificationEmail(account.Email);
            var Response = new MessageResponse
            {
                Message = "Please check your email!"
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
            if (auth == null)
            {
                throw new Exception("Something wrong!");
            }
            return Ok(auth);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost("users/{userId}/refresh-tokens")]
    public async Task<ActionResult<BaseResponse>> RefreshUserToken(int userId, [FromBody] RefreshTokenRequest refreshToken)
    {
        try
        {
            var auth = await _authService.RefreshTokens(userId, UserRoles.User, refreshToken.RefreshToken);
            if (auth == null)
            {
                throw new Exception("Something wrong!");
            }
            var response = new AuthenticationResponse
            {
                Data = auth
            };
            return Ok(response);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost("admins/{userId}/refresh-tokens")]
    public async Task<ActionResult<BaseResponse>> RefreshAdminToken(int userId, [FromBody] RefreshTokenRequest refreshToken)
    {
        try
        {
            var auth = await _authService.RefreshTokens(userId, UserRoles.Admin, refreshToken.RefreshToken);
            if (auth == null)
            {
                throw new Exception("Something wrong!");
            }
            var response = new AuthenticationResponse
            {
                Data = auth
            };
            return Ok(response);
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
                Message = "Please check your email!"
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
            if (userId == null)
            {
                return BadRequest(
                    new MessageResponse
                    {
                        Message = "missing field in token"
                    }
                );
            }
            await _authService.Logout(UserRoles.User, int.Parse(userId));
            return Ok(new MessageResponse
            {
                Message = "Logout successfully!"
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
            if (userId == null)
            {
                return BadRequest(
                    new MessageResponse
                    {
                        Message = "missing field in token"
                    }
                );
            }
            await _authService.Logout(UserRoles.Admin, int.Parse(userId));
            return Ok(new MessageResponse
            {
                Message = "Logout successfully!"
            });
        }
        catch (Exception)
        {
            throw;
        }
    }
}