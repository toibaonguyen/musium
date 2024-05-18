
using JobNet.Data;
using JobNet.Exceptions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using JobNet.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JobNet.Services;

public class CloudMessageRegistrationTokenService : ICloudMessageRegistrationTokenService, ICloudMessageTokenHandlerService
{
    private readonly string USER_IS_NOT_EXIST = "User is not exist!";
    private readonly string TOKEN_IS_NOT_EXIST = "Token is not exist!";
    private readonly ILogger<CloudMessageRegistrationTokenService> _logger;
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly NotificationSetting _notificationSetting;
    private readonly IUserService _userService;
    public CloudMessageRegistrationTokenService(IOptions<NotificationSetting> notificationOptions, ILogger<CloudMessageRegistrationTokenService> logger, JobNetDatabaseContext databaseContext, IUserService userService)
    {
        _logger = logger;
        _databaseContext = databaseContext;
        _userService = userService;
        _notificationSetting = notificationOptions.Value;
    }
    public async Task AddOrRefreshTokenAsync(int userId, string token)
    {
        try
        {
            var user = await _userService.GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            var storedToken = await _databaseContext.CloudMessageRegistrationTokens.FirstOrDefaultAsync(t => t.Token == token && t.UserId == userId);
            if (storedToken is not null)
            {
                storedToken.Timestamp = DateTime.Now;
                await _databaseContext.SaveChangesAsync();
                return;
            }
            CloudMessageRegistrationToken newToken = new()
            {
                UserId = userId,
                Token = token,
                User = user
            };
            await _databaseContext.CloudMessageRegistrationTokens.AddAsync(newToken);
            await _databaseContext.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when adding registration token!");
            throw;
        }
    }

    public async Task DeleteTokenAsync(int userId, string token)
    {
        try
        {
            var storedToken = _databaseContext.CloudMessageRegistrationTokens.FirstOrDefault(e => e.UserId == userId && e.Token == token) ?? throw new BadRequestException(TOKEN_IS_NOT_EXIST);
            _databaseContext.CloudMessageRegistrationTokens.Remove(storedToken);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when deleting registration token!");
            throw;
        }
    }

    public async Task<IList<string>> GetTokensOfUserByUserIdAsync(int userId)
    {
        try
        {
            var user = await _userService.GetUserById(userId) ?? throw new BadRequestException(USER_IS_NOT_EXIST);
            return user.CloudMessageRegistrationTokens.Select(t => t.Token).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when getting list of registration tokens!");
            throw;
        }
    }
    public async Task RemoveStaleTokensAsync()
    {
        try
        {
            var StaleTokens = _databaseContext.CloudMessageRegistrationTokens.Where(e => e.Timestamp.AddSeconds(_notificationSetting.RegistrationTokenExpirationTime) <= DateTime.Now);
            _databaseContext.CloudMessageRegistrationTokens.RemoveRange(StaleTokens);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when removing registration token!");
            throw;
        }
    }
}