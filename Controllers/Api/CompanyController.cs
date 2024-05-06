
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
public class CompanyController : ControllerBase
{
    private readonly string INVALID_TOKEN = "Invalid token!";
    private readonly ILogger<CompanyController> _logger;
    private readonly IPostService _postService;
    public CompanyController(ILogger<CompanyController> logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateNewPost([FromForm] CreatePostDTO request)
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

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{postId}")]
    public async Task<ActionResult<BaseResponse>> GetActivePostDTO(int postId)
    {
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{postId}")]
    public async Task<ActionResult<BaseResponse>> UpdatePost(int postId, [FromForm] UpdatePostDTO update)
    {
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{postId}/activation-status")]
    public async Task<ActionResult<BaseResponse>> DisablePostPost(int postId)
    {
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{postId}/comment")]
    public async Task<ActionResult<BaseResponse>> CommentPost(int postId, [FromForm] CreatePostCommentDTO comment)
    {
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{postId}/comment/{commentId}")]
    public async Task<ActionResult<BaseResponse>> EditComment(int postId, int commentId, [FromForm] CreatePostCommentDTO comment)
    {
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{postId}/reaction")]
    public async Task<ActionResult<BaseResponse>> ReactPost(int postId, [FromBody] CreatePostReactionDTO react)
    {
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpDelete]
    [Route("{postId}/reaction")]
    public async Task<ActionResult<BaseResponse>> UnReactPost(int postId, [FromBody] CreatePostReactionDTO react)
    {
    }
}
