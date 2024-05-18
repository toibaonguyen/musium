using JobNet.DTOs;
using JobNet.Enums;

namespace JobNet.Interfaces.Services;
public interface INotificationService
{
    Task CreateAndSendNotification(ResourceNotificationType notificationType, int recieverId, string content, int resourceId);
}