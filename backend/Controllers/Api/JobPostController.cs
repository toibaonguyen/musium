
using JobNet.Contants;
using JobNet.Enums;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Requests;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobPostController : ControllerBase
{
    private readonly string CREATE_SUCCESSFULLY = "Create successfully!";
    private readonly string UPDATE_SUCCESSFULLY = "Update successfully!";
    private readonly string DELETE_SUCCESSFULLY = "Delete successfully!";
    private readonly IJobPostService _jobPostService;
    private readonly ILogger<CompanyController> _logger;
    public JobPostController(ILogger<CompanyController> logger, IJobPostService jobPostService)
    {
        _jobPostService = jobPostService;
        _logger = logger;
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPut]
    [Route("{jobPostId}/status")]
    public async Task<ActionResult<BaseResponse>> ChangeStatus(int jobPostId, ChangeJobPostStatusRequest request)
    {
        try
        {
            await _jobPostService.ChangeJobPostStatus(jobPostId, request.IsActive);
            MessageResponse res = new() { Message = UPDATE_SUCCESSFULLY };
            return Ok(res);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpDelete]
    [Route("{jobPostId}")]
    public async Task<ActionResult<BaseResponse>> DeleteJobPost(int jobPostId)
    {
        try
        {
            await _jobPostService.DeleteJobPost(jobPostId);
            MessageResponse res = new() { Message = DELETE_SUCCESSFULLY };
            return Ok(res);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPut]
    [Route("{jobPostId}")]
    public async Task<ActionResult<BaseResponse>> DeleteJobPost(int jobPostId, [FromBody] UpdateJobPostRequest request)
    {
        try
        {
            await _jobPostService.UpdateJobPost(jobPostId, request.Data);
            MessageResponse res = new() { Message = UPDATE_SUCCESSFULLY };
            return Ok(res);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("list/active-and-valid")]
    public async Task<ActionResult<BaseResponse>> GetActiveAndValidJobPosts([FromQuery] int limit, [FromQuery] DateTime cursor, [FromQuery] string keyword, [FromQuery] List<string> skills, [FromQuery] List<string> locationTypes, [FromQuery] List<string> employmentTypes)
    {
        try
        {
            ListJobPostsResponse res = new()
            {
                Data = await _jobPostService.GetActiveAndValidListJobPostDTOs(limit, cursor, keyword, skills, locationTypes.Select(l => (LocationType)Enum.Parse(typeof(LocationType), l)).ToList(), employmentTypes.Select(l => (EmploymentType)Enum.Parse(typeof(EmploymentType), l)).ToList())
            };
            return Ok(res);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet]
    [Route("list")]
    public async Task<ActionResult<BaseResponse>> GetJobPosts([FromQuery] int limit, [FromQuery] DateTime cursor)
    {
        try
        {
            ListJobPostsResponse res = new()
            {
                Data = await _jobPostService.GetListJobPostDTOs(limit, cursor)
            };
            return Ok(res);
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpGet]
    [Route("list/{jobPostId}")]
    public async Task<ActionResult<BaseResponse>> GetActiveListJobPostDTOById(int jobPostId)
    {
        try
        {
            ListJobPostResponse res = new()
            {
                Data = await _jobPostService.GetActiveListJobPostDTOById(jobPostId)
            };
            return Ok(res);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    [Route("{jobPostId}")]
    public async Task<ActionResult<BaseResponse>> GetActiveJobPostDTOById(int jobPostId)
    {
        try
        {
            JobPostResponse res = new()
            {
                Data = await _jobPostService.GetActiveJobPostDTOById(jobPostId)
            };
            return Ok(res);
        }
        catch (Exception)
        {
            throw;
        }
    }
}