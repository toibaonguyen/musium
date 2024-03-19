using JobNet.DTOs;
using JobNet.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly PostsService _postsService;
    public PostController(PostsService postsService)
    {
        this._postsService = postsService;
    }

    // [HttpGet]
    // public async Task<ActionResult<List<ListUserDTO>>> GetlistOfFilteredPost()
    // {

    // }
    // [HttpGet("{id}")]
    // public async Task<ActionResult<UserDTO>> GetPostById(string id)
    // {

    // }


}