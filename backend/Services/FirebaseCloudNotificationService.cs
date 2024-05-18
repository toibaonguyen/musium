
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
    public async Task SendMultipleMessagesAsync(List<Message> messages)
    {
        try
        {
            BatchResponse response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messages);
            _logger.LogInformation(response.FailureCount, "Failure message count:");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when sending messages");
            throw;
        }
    }
    public async Task SendMessageAsync(Message message)
    {
        try
        {
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            _logger.LogInformation(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception when sending message");
            throw;
        }
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