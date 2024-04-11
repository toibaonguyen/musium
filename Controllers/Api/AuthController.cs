using JobNet.Contants;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Requests;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        this._authService = authService;
        this._logger = logger;
    }

    [HttpPost("register-user")]
    public async Task<ActionResult<BaseResponse>> RegisterUser([FromBody] RegisterUserRequest requestBody)
    {
        try
        {
            await _authService.RegisterUser(requestBody.Data);

            var res = new MessageResponse
            {
                Message = "Create successfully, please confirm email!"
            };

            return Ok(res);
        }
        catch (Exception)
        {

            throw;
        }
    }
    [HttpPost("user/login")]
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
    [HttpPost("admin/login")]
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
    [HttpPost("user/refresh-tokens")]
    public async Task<ActionResult<BaseResponse>> RefreshUserToken([FromHeader(Name = "userid")] int userId, [FromBody] RefreshTokenRequest refreshToken)
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
    [HttpPost("admin/refresh-tokens")]
    public async Task<ActionResult<BaseResponse>> RefreshAdminToken([FromHeader(Name = "userid")] int userId, [FromBody] RefreshTokenRequest refreshToken)
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
    [HttpPost("admin")]
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
    [HttpGet("confirm-email")]
    public async Task<ActionResult<BaseResponse>> ConfirmEmail(int userId, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest(new MessageResponse { Message = "Can't confirm email!" });
        }
        try
        {
            await _authService.ConfirmUser(userId, token);
            return Ok(new MessageResponse { Message = "Confirm email successfully!" });
        }
        catch (Exception)
        {
            throw;
        }
    }
}