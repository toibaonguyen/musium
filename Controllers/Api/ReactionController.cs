
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
public class ReactionController : ControllerBase
{
    [HttpGet]
    public ActionResult<BaseResponse> GetReactionTypes()
    {
        return Ok(new ReactionTypesResponse
        {
            Reactions = new List<string>(){PostReactionType.LIKE.ToString(),PostReactionType.CELEBRATE.ToString(),
    PostReactionType.SUPPORT.ToString(),
    PostReactionType.LOVE.ToString(),
    PostReactionType.INSIGHTFUL.ToString(),
    PostReactionType.FUNNY.ToString()}
        });
    }
}