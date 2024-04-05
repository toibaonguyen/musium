using JobNet.DTOs;
using JobNet.Interfaces.Services;
using JobNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;
    public UsersController(ILogger<UsersController> logger, IUserService usersService)
    {
        this._userService = usersService;
        this._logger = logger;
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<ListUserDTO>>> GetlistOfFilteredUser()
    // {

    // }
    // [HttpGet("{id}")]
    // public async Task<ActionResult<UserDTO>> GetDetailOfUser(string id)
    // {

    // }

}