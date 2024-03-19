using JobNet.DTOs;
using JobNet.Extensions;
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
    [HttpGet]
    public async Task<ActionResult<int>> Get()
    {
        try
        {
            var test = await _userService.CreateNewUser(new()
            {
                Name = "1",
                Email = "2",
                Password = "3",
                Location = "4",
                Birthday = new DateTime(7, 7, 7)
            });
            return Ok(test);
        }
        catch (Exception e)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(e.Message);
            Console.ResetColor();
            throw;
        }
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