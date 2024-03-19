using JobNet.DTOs;
using JobNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UsersService _userService;
    public UsersController(UsersService usersService)
    {
        this._userService = usersService;
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