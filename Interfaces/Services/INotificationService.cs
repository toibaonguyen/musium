using FirebaseAdmin.Messaging;

namespace JobNet.Interfaces.Services;

public interface INotificationService
{
    Task SendMessageAsync(Message message);
    Task SendMultipleMessagesAsync(List<Message> messages);
}