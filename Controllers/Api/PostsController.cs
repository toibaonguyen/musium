
using JobNet.Contants;
using JobNet.DTOs;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Requests;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly string INVALID_TOKEN = "Invalid token!";
    private readonly ILogger<PostsController> _logger;
    private readonly IPostService _postService;
    public PostsController(ILogger<PostsController> logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    public async Task<IActionResult> CreateNewPost([FromForm] CreatePostDTO request)
    {
        try
        {
            var userId = HttpContext.User.FindFirst("userId")?.Value;
            if (userId == null)
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = INVALID_TOKEN
                    }
                );
            }
            CreatedPostResponse res = new() { Data = await _postService.CreateNewPost(request, int.Parse(userId)) };
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when creating new Post!");
            throw;
        }
    }
}
