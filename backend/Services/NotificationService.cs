using FirebaseAdmin.Messaging;
using JobNet.Data;
using JobNet.Enums;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobNet.Services;

public class NotificationService : INotificationService
{
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly IFirebaseCloudNotificationService _fcmService;
    public NotificationService(JobNetDatabaseContext databaseContext, IFirebaseCloudNotificationService firebaseCloudNotificationService)
    {
        _databaseContext = databaseContext;
        _fcmService = firebaseCloudNotificationService;
    }
    public async Task CreateAndSendNotification(ResourceNotificationType notificationType, int recieverId, string content, int resourceId)
    {
        List<CloudMessageRegistrationToken> tokens = await _databaseContext.CloudMessageRegistrationTokens.Where(t => t.UserId == recieverId).ToListAsync();
        try
        {
            switch (notificationType)
            {
                case ResourceNotificationType.POST:
                    PostNotification postNotification = new()
                    {
                        Reciever = await _databaseContext.Users.FindAsync(recieverId) ?? throw new Exception("Something wrong when getting user!"),
                        Content = content,
                        Post = await _databaseContext.Posts.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting post!")
                    };
                    await _databaseContext.PostNotifications.AddAsync(postNotification);
                    await _databaseContext.SaveChangesAsync();
                    await _fcmService.SendMulticastMessageAsync(new MulticastMessage
                    {
                        Tokens = tokens.Select(t => t.Token).ToList(),
                        Data = postNotification.ToNotificationDTO().ToStringDictionary(),
                        Notification = new FirebaseAdmin.Messaging.Notification()
                        {
                            Title = "New post",
                            Body = postNotification.Content,
                        },
                    });
                    break;
                case ResourceNotificationType.CONNECTION:
                    ConnectionRequestNotification connectionNotification = new()
                    {
                        Reciever = await _databaseContext.Users.FindAsync(recieverId) ?? throw new Exception("Something wrong when getting user!"),
                        Content = content,
                        Connection = await _databaseContext.Connections.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting connection!")
                    };
                    await _databaseContext.ConnectionRequestNotifications.AddAsync(connectionNotification);
                    await _databaseContext.SaveChangesAsync();
                    await _fcmService.SendMulticastMessageAsync(new MulticastMessage
                    {
                        Tokens = tokens.Select(t => t.Token).ToList(),
                        Data = connectionNotification.ToNotificationDTO().ToStringDictionary(),
                        Notification = new FirebaseAdmin.Messaging.Notification()
                        {
                            Title = "New connection request",
                            Body = connectionNotification.Content,
                        },
                    });
                    break;
                case ResourceNotificationType.MESSAGE:
                    MessageNotification messagenotification = new()
                    {
                        Reciever = await _databaseContext.Users.FindAsync(recieverId) ?? throw new Exception("Something wrong when getting user!"),
                        Content = content,
                        Message = await _databaseContext.Messages.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting message!")
                    };
                    await _databaseContext.MessageNotifications.AddAsync(messagenotification);
                    await _databaseContext.SaveChangesAsync();
                    await _fcmService.SendMulticastMessageAsync(new MulticastMessage
                    {
                        Tokens = tokens.Select(t => t.Token).ToList(),
                        Notification = new FirebaseAdmin.Messaging.Notification()
                        {
                            Title = "New Message"
                        },
                        Data = messagenotification.ToNotificationDTO().ToStringDictionary(),
                    });
                    break;
                case ResourceNotificationType.JOBPOST:
                    JobPostNotification jobPostNotification = new()
                    {
                        Reciever = await _databaseContext.Users.FindAsync(recieverId) ?? throw new Exception("Something wrong when getting user!"),
                        Content = content,
                        JobPost = await _databaseContext.JobPosts.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting message!")
                    };
                    await _databaseContext.JobPostNotifications.AddAsync(jobPostNotification);
                    await _databaseContext.SaveChangesAsync();
                    await _fcmService.SendMulticastMessageAsync(new MulticastMessage
                    {
                        Tokens = tokens.Select(t => t.Token).ToList(),
                        Notification = new FirebaseAdmin.Messaging.Notification()
                        {
                            Title = "New job opportunities",
                            Body = jobPostNotification.Content
                        },
                        Data = jobPostNotification.ToNotificationDTO().ToStringDictionary(),
                    });
                    break;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}