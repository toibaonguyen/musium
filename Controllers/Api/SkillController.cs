
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
    private readonly ISkillService _skillService;
    private readonly ILogger<SkillController> _logger;
    public SkillController(ISkillService skillService, ILogger<SkillController> logger)
    {
        _skillService = skillService;
        _logger = logger;
    }

    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateNewSkill([FromForm] CreateSkillDTO skill)
    {
        try
        {
            await _skillService.CreateNewSkill(skill.Name);
            return Created();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    [HttpGet]
    public async Task<ActionResult<BaseResponse>> GetListSkills([FromQuery] int limit)
    {
        try
        {
            return Ok(new SkillsResponse { Skills = await _skillService.GetSkillDTOs(limit) });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpGet]
    public async Task<ActionResult<BaseResponse>> GetListSkills([FromQuery] string similar, [FromQuery] int limit)
    {
        try
        {
            return Ok(new SkillsResponse { Skills = await _skillService.GetSkillDTOs(similar, limit) });
        }
        catch (Exception)
        {
            throw;
        }
    }
}