namespace JobNet.Interfaces.Services;

public interface ICloudMessageRegistrationTokenService
{
    Task AddOrRefreshTokenAsync(int userId, string token);
    Task RemoveStaleTokensAsync();
    Task DeleteTokenAsync(int userId, string token);
    Task<IList<string>> GetTokensOfUserByUserIdAsync(int userId);
}