using JobNet.Contants;
using JobNet.DTOs;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Requests;
using JobNet.Models.Core.Responses;
using JobNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly string UPDATE_SUCCESSFULLY = "Update successfully!";
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;
    public UsersController(ILogger<UsersController> logger, IUserService usersService)
    {
        _userService = usersService;
        _logger = logger;
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/name")]
    public async Task<ActionResult<BaseResponse>> ChangeName(int userId, [FromBody] UpdateUserNameRequest request)
    {
        try
        {
            await _userService.ChangeUserName(userId, request.Name);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/avatar")]
    public async Task<ActionResult<BaseResponse>> ChangeAvatar(int userId, [FromBody] UpdateUserAvatarRequest request)
    {
        try
        {
            await _userService.ChangeUserAvatar(userId, request.Avatar);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }

    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/background-image")]
    public async Task<ActionResult<BaseResponse>> ChangeBackgroundImage(int userId, [FromBody] UpdateUserBackgroundImageRequest request)
    {
        try
        {
            await _userService.ChangeUserBackground(userId, request.BackgroundImage);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/location")]
    public async Task<ActionResult<BaseResponse>> ChangeLocation(int userId, [FromBody] UpdateUserLocationRequest request)
    {
        try
        {
            await _userService.ChangeLocation(userId, request.Location);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });

        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/birthday")]
    public async Task<ActionResult<BaseResponse>> ChangeBirthday(int userId, [FromBody] UpdateUserBirthdayRequest request)
    {
        try
        {
            await _userService.ChangeBirthday(userId, request.Birthday);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });

        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/experiences")]
    public async Task<ActionResult<BaseResponse>> ChangeExperiences(int userId, [FromBody] UpdateUserExperiencesRequest request)
    {
        try
        {
            await _userService.ChangeExperiences(userId, request.Experiences);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/certifications")]
    public async Task<ActionResult<BaseResponse>> ChangeCertifications(int userId, [FromBody] UpdateUserCertificationsRequest request)
    {
        try
        {
            await _userService.ChangeCertifications(userId, request.Certifications);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/educations")]
    public async Task<ActionResult<BaseResponse>> ChangeEducations(int userId, [FromBody] UpdateUserEducationsRequest request)
    {
        try
        {
            await _userService.ChangeEducations(userId, request.Educations);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/skills")]
    public async Task<ActionResult<BaseResponse>> ChangeSkills(int userId, [FromBody] UpdateUserSkillsRequest request)
    {
        try
        {
            await _userService.ChangeSkills(userId, request.Skills);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpGet]
    [Route("{userId}/profile")]
    public async Task<ActionResult<BaseResponse>> GetActiveProfileUserDTOByUserId(int userId)
    {
        try
        {
            return Ok(new UserProfileResponse { Data = await _userService.GetProfileUserDTOById(userId) });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPut]
    [Route("{userId}/activation-status")]
    public Task<ActionResult<BaseResponse>> ChangeUserActivationStatus(int userId, [FromBody] UpdateActivationStatusRequest request)
    {
        try
        {
            return Ok(new UserProfileResponse { Data = await _userService.GetProfileUserDTOById(userId) });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/connections")]
    public Task<ActionResult<BaseResponse>> GetConnectionsOfUserById(int userId)
    {

    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/connection-requests")]
    public Task<ActionResult<BaseResponse>> GetConnectionRequestsOfUser(int userId)
    {

    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/following-companies")]
    public Task<ActionResult<BaseResponse>> GetFollowingCompanieDTOsOfUserByUserId(int userId)
    {

    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{userId}/connections")]
    public Task<ActionResult<BaseResponse>> RequestConnectionToUserWithUserId(int userId)
    {

    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/connections/{connectionId}/status")]
    public Task<ActionResult<BaseResponse>> ConfirmOrRejectConnectionRequest(int userId, int connectionId, [FromBody] UpdateConnectionStatusRequest request)
    {

    }
    [HttpGet]
    [Route("{userId}/posts")]
    public Task<ActionResult<BaseResponse>> GetPostDTOsOfUser(int userId)
    {

    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/notifications")]
    public Task<ActionResult<BaseResponse>> GetNotificationDTOsOfUser(int userId, [FromQuery] int limit)
    {

    }
}