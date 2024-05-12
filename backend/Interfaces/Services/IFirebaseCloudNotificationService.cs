using FirebaseAdmin.Messaging;

namespace JobNet.Interfaces.Services;

public interface IFirebaseCloudNotificationService
{
    Task SendMessageAsync(Message message);
    Task SendMultipleMessagesAsync(List<Message> messages);
}