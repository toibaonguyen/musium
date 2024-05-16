using JobNet.DTOs;
using JobNet.Enums;
using JobNet.Interfaces.Services;

namespace JobNet.Services;

public class NotificationService : INotificationService
{
    public Task<NotificationDTO> CreateAndSendNotification(ResourceNotificationType notificationType, int senderId, int recieverId, string content, int resourceId)
    {
        throw new NotImplementedException();
    }
}