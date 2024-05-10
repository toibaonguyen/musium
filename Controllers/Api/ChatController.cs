
using System.Security.Claims;
using JobNet.Contants;
using JobNet.DTOs;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IPrivateChatService _privateChatService;
    private readonly string INVALID_TOKEN = "Invalid token!";
    private readonly string DO_NOT_HAVE_PERMISSION = "Do not have permission!";
    private readonly string SENT_MESSAGE_SUCCESSFULLY = "message sent!";
    public ChatController(IPrivateChatService privateChatService)
    {
        _privateChatService = privateChatService;
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("conversations")]
    public async Task<ActionResult<BaseResponse>> GetConversationsOfUser([FromQuery] int limit, [FromQuery] DateTime cursor)
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = INVALID_TOKEN
                    }
                );
            }

            return Ok(new ConversationBoxsResponse { Data = await _privateChatService.GetConversationBoxsOfUserOrderByLastMessageSentTimeDesc(int.Parse(userId), limit, cursor) });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("conversations/{conversationId}/messages")]
    public async Task<ActionResult<BaseResponse>> GetPrivateMessagesOfConersation(int conversationId, [FromQuery] int limit, [FromQuery] DateTime cursor)
    {
        try
        {
            var authUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (authUserId is null)
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = INVALID_TOKEN
                    }
                );
            }
            if (!await _privateChatService.CheckIfUserIsInConversation(int.Parse(authUserId), conversationId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }

            return Ok(new ChatMessagesResponse { Data = await _privateChatService.GetMessagesOfConversationOrderBySentTimeDesc(conversationId, limit, cursor) });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{userId}/messages")]
    public async Task<ActionResult<BaseResponse>> SendMessage(int userId, [FromForm] CreateMessageDTO message)
    {
        try
        {
            var authUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (authUserId is null)
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = INVALID_TOKEN
                    }
                );
            }
            await _privateChatService.SendPrivateMessage(int.Parse(authUserId), userId, message);

            return Ok(new MessageResponse { Message = SENT_MESSAGE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
}