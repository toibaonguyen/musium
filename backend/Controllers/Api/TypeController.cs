
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
public class TypeController : ControllerBase
{
    [HttpGet]
    [Route("reaction-type")]
    public ActionResult<BaseResponse> GetReactionTypes()
    {
        return Ok(new ReactionTypesResponse
        {
            Reactions = [PostReactionType.LIKE.ToString(),PostReactionType.CELEBRATE.ToString(),
    PostReactionType.SUPPORT.ToString(),
    PostReactionType.LOVE.ToString(),
    PostReactionType.INSIGHTFUL.ToString(),
    PostReactionType.FUNNY.ToString()]
        });
    }
    [HttpGet]
    [Route("employment-type")]
    public ActionResult<BaseResponse> GetEmploymentTypes()
    {
        return Ok(new ReactionTypesResponse
        {
            Reactions = [EmploymentType.FULLTIME.ToString(),
    EmploymentType.PARTTIME.ToString(),
    EmploymentType.SELF_EMPLOYED.ToString(),
    EmploymentType.FREELANCE.ToString(),
    EmploymentType.CONTRACT.ToString(),
    EmploymentType.INTERNSHIP.ToString(),
    EmploymentType.APPRENTICESHIP.ToString(),
    EmploymentType.SEASONAL.ToString()]
        });
    }
    [HttpGet]
    [Route("location-type")]
    public ActionResult<BaseResponse> GetLocationTypes()
    {
        return Ok(new ReactionTypesResponse
        {
            Reactions = [LocationType.ON_SITE.ToString(),
    LocationType.HYBRID.ToString(),
    LocationType.REMOTE.ToString()]
        });
    }
    [HttpGet]
    [Route("connection-request-status-type")]
    public ActionResult<BaseResponse> GetConnectionRequestTypes()
    {
        return Ok(new ReactionTypesResponse
        {
            Reactions = [ConnectionRequestStatusType.PENDING.ToString(),
    ConnectionRequestStatusType.ACCEPT.ToString(),
    ConnectionRequestStatusType.REJECT.ToString()]
        });
    }
}