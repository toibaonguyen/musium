using JobNet.Contants;
using JobNet.Interfaces.Services;
using JobNet.Models.Core.Requests;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace JobNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CloudMessageRegistrationTokenController : ControllerBase
{
    private readonly string INVALID_TOKEN = "Invalid token!";
    private readonly string TOKEN_IS_STORED_SUCCESSFULLY = "Token is stored successfully!";
    private readonly string TOKEN_IS_STORED = "STORED";
    private readonly string TOKEN_IS_NOT_STORED = "NOT_STORED";


    private readonly ILogger<CloudMessageRegistrationTokenController> _logger;
    private readonly ICloudMessageRegistrationTokenService _cloudMessageRegistrationTokenService;

    public CloudMessageRegistrationTokenController(ILogger<CloudMessageRegistrationTokenController> logger, ICloudMessageRegistrationTokenService cloudMessageRegistrationTokenService)
    {
        _logger = logger;
        _cloudMessageRegistrationTokenService = cloudMessageRegistrationTokenService;
    }

    [Authorize(Policy = IdentityData.UserPolicyName)]
    [HttpPut]
    public async Task<ActionResult<BaseResponse>> AddOrRefreshToken([FromBody] StoreCloudMessageRegistrationRequest request)
    {
        try
        {
            var userId = HttpContext.User.FindFirst("userId")?.Value;
            if (userId == null)
            {
                return Unauthorized(
                    new MessageResponse
                    {
                        Message = INVALID_TOKEN
                    }
                );
            }
            await _cloudMessageRegistrationTokenService.AddOrRefreshTokenAsync(int.Parse(userId), request.Data.Token);
            return Ok(new MessageResponse { Message = TOKEN_IS_STORED_SUCCESSFULLY });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception when adding token");
            throw;
        }
    }


}