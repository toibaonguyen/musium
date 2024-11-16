


using JobNet.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("[controller]")]
[Controller]
public class AuthenticationController : ControllerBase
{
    private readonly string VERIFY_EMAIL_FAIL = "Can't verify email!";
    private readonly string VERIFY_EMAIL_SUCCESSFULLY = "Verify email successfully!";
    private readonly string CONFIRM_RESET_PASSWORD_FAIL = "Can't confirm reset password!";
    private readonly string CONFIRM_RESET_PASSWORD_SUCCESSFULLY = "Confirm reset password successfully!";
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
            return BadRequest(VERIFY_EMAIL_FAIL);
        }
        try
        {
            await _authService.ConfirmUser(userId, token);
            return Ok(VERIFY_EMAIL_SUCCESSFULLY);
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
            return Unauthorized(CONFIRM_RESET_PASSWORD_FAIL);
        }
        try
        {
            await _authService.ConfirmResetPassword(userId, token);
            return Ok(CONFIRM_RESET_PASSWORD_SUCCESSFULLY);
        }
        catch (Exception)
        {
            throw;
        }
    }
}