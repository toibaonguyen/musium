
using System.Security.Claims;
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
    private readonly string CREATE_SUCCESSFULLY = "Create successfully!";
    private readonly string UPDATE_SUCCESSFULLY = "Update successfully!";
    private readonly string INVALID_TOKEN = "Invalid token!";
    private readonly ICompanyService _companyService;
    private readonly IJobPostService _jobPostService;
    private readonly ILogger<CompanyController> _logger;
    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, IJobPostService jobPostService)
    {
        _jobPostService = jobPostService;
        _companyService = companyService;
        _logger = logger;
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    public async Task<ActionResult<BaseResponse>> GetListCompanyDTOs([FromQuery] int limit, [FromQuery] int cursor, [FromQuery] string keyword)
    {
        try
        {
            await _companyService.GetActiveListCompanyDTOs(limit, cursor, keyword);
            return Ok(new MessageResponse { Message = CREATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{companyId}/active")]
    public async Task<ActionResult<BaseResponse>> GetActiveCompanyDTO(int companyId)
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
            return Ok(new CompanyResponse { Data = await _companyService.GetActiveCompanyDTObyId(companyId), IsFollowing = await _companyService.CheckIfUserIsFollowCompany(int.Parse(userId), companyId) });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet]
    [Route("{companyId}")]
    public async Task<ActionResult<BaseResponse>> GetCompanyDTO(int companyId)
    {
        try
        {
            return Ok(new CompanyResponse { Data = await _companyService.GetCompanyDTOById(companyId), IsFollowing = false });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateCompany([FromBody] CreateCompanyDTO request)
    {
        try
        {
            await _companyService.CreateCompany(request);
            return Ok(new MessageResponse { Message = CREATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPut]
    [Route("{companyId}")]
    public async Task<ActionResult<BaseResponse>> UpdateCompany(int companyId, [FromBody] CreateCompanyDTO request)
    {
        try
        {
            await _companyService.UpdateCompany(companyId, request);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPut]
    [Route("{companyId}/activation-status")]
    public async Task<ActionResult<BaseResponse>> UpdateCompany(int companyId, [FromBody] ChangeCompanyActivationStatus request)
    {
        try
        {
            await _companyService.UpdateCompanyStatus(companyId, request.IsActive);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [HttpGet]
    [Route("{companyId}/ListJobPosts/valid-and-active")]
    public async Task<ActionResult<BaseResponse>> UpdateCompany(int companyId, [FromQuery] int limit, [FromQuery] DateTime cursor)
    {
        try
        {
            return Ok(new ListJobPostsResponse { Data = await _jobPostService.GetActiveAndValidListJobPostDTOsOfCompany(limit, cursor, companyId) });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet]
    [Route("{companyId}/ListJobPosts")]
    public async Task<ActionResult<BaseResponse>> GetJobPost(int companyId, [FromQuery] int limit, [FromQuery] DateTime cursor)
    {
        try
        {
            return Ok(new ListJobPostsResponse { Data = await _jobPostService.GetListJobPostDTOsOfCompany(limit, cursor, companyId) });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    [Route("{companyId}/JobPosts")]
    public async Task<ActionResult<BaseResponse>> CreateJobPost(int companyId, [FromBody] CreateJobPostRequest request)
    {
        try
        {
            return Ok(new ListJobPostsResponse { Data = [await _jobPostService.CreateJobPostAndSendToticationToFollowers(companyId, request.Data)] });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{companyId}/follow")]
    public async Task<ActionResult<BaseResponse>> Follow(int companyId)
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
            if (await _companyService.CheckIfUserIsFollowCompany(int.Parse(userId), companyId))
            {
                await _companyService.FollowCompany(int.Parse(userId), companyId);
            }
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when update Company");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{companyId}/unfollow")]
    public async Task<ActionResult<BaseResponse>> UnFollow(int companyId)
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
            if (await _companyService.CheckIfUserIsFollowCompany(int.Parse(userId), companyId))
            {
                await _companyService.UnFollowCompany(int.Parse(userId), companyId);
            }
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
}