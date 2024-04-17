


using JobNet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("[controller]")]
[Controller]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthService _authService;
    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail(int userId, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Can't verify email!");
        }
        try
        {
            await _authService.ConfirmUser(userId, token);
            return Ok("Verify email successfully!");
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpGet("confirm-reset-password")]
    public async Task<IActionResult> ConfirmResetPassword(int userId, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("Can't reset password");
        }
        try
        {
            await _authService.ConfirmResetPassword(userId, token);
            return Ok("Reset password success your new password is sent to your email, check it out!");
        }
        catch (Exception)
        {
            throw;
        }
    }
}