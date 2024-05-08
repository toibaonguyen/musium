
using Azure.Storage.Blobs.Models;
using JobNet.Contants;
using JobNet.DTOs;
using JobNet.Enums;
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
    private readonly string INVALID_POST = "Invalid post!";
    private readonly string DO_NOT_HAVE_PERMISSION = "Do not have permission!";
    private readonly string UPDATE_SUCCESSFULLY = "Update successfully!";
    private readonly ILogger<PostsController> _logger;
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;
    private readonly IPostReactService _postReactService;
    public PostsController(ILogger<PostsController> logger, IPostService postService, ICommentService commentService, IPostReactService postReactService)
    {
        _logger = logger;
        _postService = postService;
        _commentService = commentService;
        _postReactService = postReactService;
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
    [HttpGet]
    [Route("{postId}")]
    public async Task<ActionResult<BaseResponse>> GetActivePostDTO(int postId)
    {
        try
        {

            PostResponse res = new() { Data = await _postService.GetActivePostDTOById(postId) };
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when getting active Post!");
            throw;
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{postId}")]
    public async Task<ActionResult<BaseResponse>> UpdatePost(int postId, [FromForm] UpdatePostDTO update)
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
            var post = await _postService.GetPostById(postId);
            if (post is null || !post.IsActive)
            {
                return BadRequest(new MessageResponse { Message = INVALID_POST });
            }
            if (post.OwnerId != int.Parse(userId))
            {
                return Unauthorized(new MessageResponse { Message = DO_NOT_HAVE_PERMISSION });
            }


            PostResponse res = new() { Data = await _postService.UpdatePost(postId, update) };
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when updating Post!");
            throw;
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{postId}/disable-activation-status")]
    public async Task<ActionResult<BaseResponse>> DisablePostPost(int postId)
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
            var post = await _postService.GetPostById(postId);
            if (post is null || !post.IsActive)
            {
                return BadRequest(new MessageResponse { Message = INVALID_POST });
            }
            if (post.OwnerId != int.Parse(userId))
            {
                return Unauthorized(new MessageResponse { Message = DO_NOT_HAVE_PERMISSION });
            }
            await _postService.DisablePost(postId);
            MessageResponse res = new() { Message = UPDATE_SUCCESSFULLY };
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when disable Post!");
            throw;
        }
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{postId}/comments")]
    public async Task<ActionResult<BaseResponse>> CommentPost(int postId, [FromForm] CreatePostCommentDTO comment)
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
            var post = await _postService.GetPostById(postId);
            if (post is null || !post.IsActive)
            {
                return BadRequest(new MessageResponse { Message = INVALID_POST });
            }
            CommentDTO newComment = await _commentService.CommentToPost(int.Parse(userId), postId, comment);

            return Ok(new CommentResponse { Data = newComment });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when commenting to Post!");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{postId}/comments/{commentId}")]
    public async Task<ActionResult<BaseResponse>> EditComment(int postId, int commentId, [FromForm] CreatePostCommentDTO comment)
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
            var post = await _postService.GetPostById(postId);
            if (post is null || !post.IsActive)
            {
                return BadRequest(new MessageResponse { Message = INVALID_POST });
            }
            CommentDTO newComment = await _commentService.UpdateComment(int.Parse(userId), comment);

            return Ok(new CommentResponse { Data = newComment });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when commenting to Post!");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{postId}/reaction")]
    public async Task<ActionResult<BaseResponse>> ReactPost(int postId, [FromBody] CreatePostReactionDTO react)
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
            var post = await _postService.GetPostById(postId);
            if (post is null || !post.IsActive)
            {
                return BadRequest(new MessageResponse { Message = INVALID_POST });
            }
            PostReactionType reaction;
            try
            {
                Enum.TryParse(react.React, out reaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return BadRequest(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
            }
            await _postReactService.ReactToPost(int.Parse(userId), postId, reaction);

            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when react to Post!");
            throw;
        }

    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpDelete]
    [Route("{postId}/reaction")]
    public async Task<ActionResult<BaseResponse>> UnReactPost(int postId)
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
            var post = await _postService.GetPostById(postId);
            if (post is null || !post.IsActive)
            {
                return BadRequest(new MessageResponse { Message = INVALID_POST });
            }

            await _postReactService.DeteleReact(int.Parse(userId), postId);

            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when unreacting to Post!");
            throw;
        }
    }
}
