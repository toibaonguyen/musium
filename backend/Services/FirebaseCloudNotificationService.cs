
using FirebaseAdmin.Messaging;
using JobNet.Interfaces.Services;

namespace JobNet.Services;

public class FirebaseCloudNotificationService : IFirebaseCloudNotificationService
{
    private readonly ILogger<FirebaseCloudNotificationService> _logger;
    public FirebaseCloudNotificationService(ILogger<FirebaseCloudNotificationService> logger)
    {
        _logger = logger;
    }

    public async Task SendMulticastMessageAsync(MulticastMessage message)
    {
        try
        {
            var response = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message);
            _logger.LogInformation(response.FailureCount, "Failure message count:");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when sending message");
            throw;
        }
    }
}