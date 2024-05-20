using JobNet.DTOs;
using JobNet.Enums;

namespace JobNet.Interfaces.Services;
public interface INotificationService
{
    Task CreateAndSendNotification(ResourceNotificationType notificationType, int[] recieverIds, string content, int resourceId);
}