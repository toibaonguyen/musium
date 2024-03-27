using JobNet.DTOs;
using JobNet.Extensions;
using JobNet.Models.Entities;
using JobNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UsersService _userService;
    public AuthController(UsersService usersService)
    {
        this._userService = usersService;
    }
    // [HttpPost("register-user")]
    // public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] CreateUserDTO newUser)
    // {

    //     return Ok();
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