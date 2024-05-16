
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
    private readonly ICompanyService _companyService;
    private readonly IJobPostService _jobPostService;
    private readonly ILogger<CompanyController> _logger;
    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, IJobPostService jobPostService)
    {
        _jobPostService = jobPostService;
        _companyService = companyService;
        _logger = logger;
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
            return Ok(new ListJobPostsResponse { Data = [await _jobPostService.CreateJobPost(companyId, request.Data)] });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when create Company");
            throw;
        }
    }
}