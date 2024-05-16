using JobNet.DTOs;
using JobNet.Enums;

namespace JobNet.Interfaces.Services;
public interface INotificationService
{
    Task<NotificationDTO> CreateAndSendNotification(ResourceNotificationType notificationType, int senderId, int recieverId, string content, int resourceId);
}