using JobNet.DTOs;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Requests;
using JobNet.Models.Core.Responses;
using JobNet.Models.Entities;
using JobNet.Services;
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

    // [HttpPost("register-user")]
    // public async Task<ActionResult<BaseResponse>> RegisterUser([FromBody] RegisterUserRequest requestBody)
    // {
    //     try
    //     {

    //     }
    //     catch (Exception e)
    //     {

    //         throw;
    //     }
    // }
    // [HttpPost("login-user")]
    // public async Task<ActionResult> LoginAsUser([FromBody] AccountDTO account)
    // {

    // }
    // [HttpPost("login-admin")]
    // public async Task<ActionResult> LoginAsAdmin([FromBody] AccountDTO account)
    // {

    // }

}