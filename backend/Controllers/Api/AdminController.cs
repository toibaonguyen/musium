
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
public class AdminController : ControllerBase
{
    private readonly string UPDATE_SUCCESSFULLY = "update successfully!";
    private readonly IAdminService _adminService;
    private readonly ILogger<AdminController> _logger;
    public AdminController(IAdminService adminService, ILogger<AdminController> logger)
    {
        _adminService = adminService;
        _logger = logger;
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateAdmin([FromBody] CreateAdminRequest request)
    {
        try
        {
            AdminDTO admin = await _adminService.CreateNewAdmin(request.Data);
            return Ok(new AdminResponse { Data = admin });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error when creating admin!");
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPut]
    [Route("{adminId}/activation-status")]
    public async Task<ActionResult<BaseResponse>> ChangeActivationStatus(int adminId, [FromBody] UpdateActivationStatusRequest request)
    {
        try
        {
            await _adminService.ChangeActiveStatus(adminId, request.IsActive);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error when updating admin!");
            throw;
        }
    }
}