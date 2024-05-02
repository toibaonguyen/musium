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
        _userService = usersService;
        _logger = logger;
    }

}