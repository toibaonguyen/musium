
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
public class SkillController : ControllerBase
{
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateNewSkill([FromForm] CreateSkillDTO skill)
    {
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpGet]
    public async Task<ActionResult<BaseResponse>> GetListSkills([FromForm] CreateSkillDTO skill, [FromQuery] int limit, [FromQuery] string orderby)
    {
    }
}