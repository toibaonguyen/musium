using FirebaseAdmin.Messaging;

namespace JobNet.Interfaces.Services;

public interface IFirebaseCloudNotificationService
{
    Task SendMulticastMessageAsync(MulticastMessage message);
}