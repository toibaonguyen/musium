namespace JobNet.Interfaces.Services;

public interface ICloudMessageTokenHandlerService
{
    Task RemoveStaleTokensAsync();
}