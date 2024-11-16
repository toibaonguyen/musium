using System.Security.Claims;
using JobNet.Contants;
using JobNet.DTOs;
using JobNet.Enums;
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
    private readonly string DO_NOT_HAVE_PERMISSION = "Do not have permission!";
    private readonly string INVALID_TOKEN = "Invalid token!";
    private readonly string INVALID__COONECTION_STATUS = "Invalid connection status!";
    private readonly IUserService _userService;
    private readonly IConnectionService _connectionService;
    private readonly ILogger<UsersController> _logger;
    public UsersController(ILogger<UsersController> logger, IUserService usersService, IConnectionService connectionService)
    {

        _userService = usersService;
        _logger = logger;
        _connectionService = connectionService;
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/name")]
    public async Task<ActionResult<BaseResponse>> ChangeName(int userId, [FromBody] UpdateUserNameRequest request)
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new UserProfileResponse
            {
                Data = await _userService.ChangeUserName(userId, request.Name),
                IsConnected = false
            });
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new UserProfileResponse { Data = await _userService.ChangeUserAvatar(userId, request.Avatar), IsConnected = false });
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new UserProfileResponse { Data = await _userService.ChangeUserBackground(userId, request.BackgroundImage), IsConnected = false });
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new UserProfileResponse { Data = await _userService.ChangeLocation(userId, request.Location), IsConnected = false });

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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new UserProfileResponse { Data = await _userService.ChangeBirthday(userId, request.Birthday), IsConnected = false });


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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            await _userService.ChangeSkills(userId, request.Skills);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/profile")]
    public async Task<ActionResult<BaseResponse>> GetActiveProfileUserDTOByUserId(int userId)
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
        try
        {
            ProfileUserDTO? userProfile = await _userService.GetActiveProfileUserDTOById(userId);
            bool isConnected = false;
            if (userProfile != null && userId != int.Parse(authUserId))
            {
                isConnected = await _connectionService.CheckIfIsConnected(userId, int.Parse(authUserId));
            }
            return Ok(new UserProfileResponse { Data = userProfile, IsConnected = isConnected });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.AdminPolicyName)]
    [HttpPut]
    [Route("{userId}/activation-status")]
    public async Task<ActionResult<BaseResponse>> ChangeUserActivationStatus(int userId, [FromBody] UpdateActivationStatusRequest request)
    {
        try
        {
            await _userService.ChangeActiveStatus(userId, request.IsActive);
            return Ok(new MessageResponse { Message = UPDATE_SUCCESSFULLY });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/connections")]
    public async Task<ActionResult<BaseResponse>> GetConnectionDTOsOfUserById(int userId, [FromQuery] int limit, [FromQuery] int offset)
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new ConnectionsResponse { Data = (await _userService.GetConnectionDTOsOfUserByUserId(userId, limit, offset)).ToList() });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/connection-requests")]
    public async Task<ActionResult<BaseResponse>> GetConnectionRequestsOfUser(int userId, [FromQuery] int limit, [FromQuery] DateTime cursor)
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new ConnectionRequestsResponse { Data = (await _userService.GetConnectionRequestDTOsOfUserByUserId(userId, limit, cursor)).ToList() });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpGet]
    [Route("{userId}/following-companies")]
    public async Task<ActionResult<BaseResponse>> GetFollowingCompanieDTOsOfUserByUserId(int userId)
    {
        try
        {
            return Ok(new ListFollowingCompaniesResponse { Data = (await _userService.GetListFollowingCompanyDTOsOfUserByUserId(userId)).ToList() });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPost]
    [Route("{userId}/connections")]
    public async Task<ActionResult<BaseResponse>> RequestConnectionToUserWithUserId(int userId)
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
            if (userId == int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            await _connectionService.CreateConnectionAndSendNotificationToReciever(int.Parse(authUserId), userId);
            return Ok();
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    [Route("{userId}/connections/{connectionId}/status")]
    public async Task<ActionResult<BaseResponse>> ConfirmOrRejectConnectionRequest(int userId, int connectionId, [FromBody] UpdateConnectionStatusRequest request)
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            ConnectionRequestStatusType status;
            try
            {
                Enum.TryParse(request.Status, out status);
            }
            catch (Exception)
            {
                return BadRequest(new MessageResponse { Message = INVALID__COONECTION_STATUS });
            }
            await _connectionService.UpdateConnectionRequestStatus(connectionId, status);
            return Ok();
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpGet]
    [Route("{userId}/posts")]
    public async Task<ActionResult<BaseResponse>> GetPostDTOsOfUser(int userId, [FromQuery] int limit, [FromQuery] int offset)
    {
        try
        {
            return Ok(new PostsResponse { Data = (await _userService.GetActivePostDTOsOfUserByUserId(userId, limit, offset)).ToList() });
        }
        catch (Exception)
        {
            throw;
        }
    }
    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpGet]
    [Route("{userId}/notifications")]
    public async Task<ActionResult<BaseResponse>> GetNotificationDTOsOfUser(int userId, [FromQuery] int limit, [FromQuery] DateTime cursor)
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
            if (userId != int.Parse(authUserId))
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = DO_NOT_HAVE_PERMISSION
                    }
                );
            }
            return Ok(new NotificationsResponse { Data = (await _userService.GetNotificationDTOsOfUserByUserId(userId, limit, cursor)).ToList() });
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    [Route("active")]
    public async Task<ActionResult<BaseResponse>> GetListUSerDTOs([FromQuery] int limit, [FromQuery] DateTime cursor, [FromQuery] string? keyword = null)
    {
        try
        {
            return Ok(new ListUsersResponse { Data = (await _userService.GetActiveListUserDTOs(limit, cursor, keyword)).ToList() });
        }
        catch (Exception)
        {
            throw;
        }
    }
}