using System.Text;
using System.Text.Json;
using FirebaseAdmin.Messaging;
using JobNet.Data;
using JobNet.Enums;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

namespace JobNet.Services;

public class NotificationService : INotificationService
{
    private readonly JobNetDatabaseContext _databaseContext;
    private readonly IRabbitMqService _rabbitMqService;
    public NotificationService(JobNetDatabaseContext databaseContext, IRabbitMqService rabbitMqService)
    {
        _databaseContext = databaseContext;
        _rabbitMqService = rabbitMqService;
    }
    public async Task CreateAndSendNotification(ResourceNotificationType notificationType, int[] recieverIds, string content, int resourceId)
    {
        List<CloudMessageRegistrationToken> tokens = await _databaseContext.CloudMessageRegistrationTokens.Where(t => recieverIds.Contains(t.Id) && t.User.IsActive).ToListAsync();
        try
        {
            if (tokens.Count == 0) return;
            var connection = _rabbitMqService.CreateConnection();
            var model = connection.CreateModel();
            switch (notificationType)
            {
                case ResourceNotificationType.POST:
                    List<PostNotification> postNotifications = [];
                    foreach (var token in tokens)
                    {

                        PostNotification postNotification = new()
                        {
                            Reciever = await _databaseContext.Users.FindAsync(token.UserId) ?? throw new Exception("Something wrong when getting user!"),
                            Content = content,
                            Post = await _databaseContext.Posts.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting post!")
                        };
                        postNotifications.Add(postNotification);
                    }
                    await _databaseContext.PostNotifications.AddRangeAsync(postNotifications);
                    await _databaseContext.SaveChangesAsync();
                    model.BasicPublish("NotificationExchange",
                                 string.Empty,
                                 basicProperties: null,
                                 body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new MulticastMessage
                                 {
                                     Tokens = tokens.Select(t => t.Token).ToList(),
                                     Data = postNotifications.FirstOrDefault()?.ToNotificationDTO().ToStringDictionary(),
                                     Notification = new FirebaseAdmin.Messaging.Notification()
                                     {
                                         Title = "New post",
                                         Body = postNotifications.FirstOrDefault()?.Content,
                                     },
                                 })));

                    break;
                case ResourceNotificationType.CONNECTION:
                    List<ConnectionRequestNotification> connectionNotifications = [];
                    foreach (var token in tokens)
                    {

                        ConnectionRequestNotification connectionNotification = new()
                        {
                            Reciever = await _databaseContext.Users.FindAsync(token.UserId) ?? throw new Exception("Something wrong when getting user!"),
                            Content = content,
                            Connection = await _databaseContext.Connections.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting connection!")
                        };
                        connectionNotifications.Add(connectionNotification);
                    }
                    await _databaseContext.ConnectionRequestNotifications.AddRangeAsync(connectionNotifications);
                    await _databaseContext.SaveChangesAsync();
                    model.BasicPublish("NotificationExchange",
                                 string.Empty,
                                 basicProperties: null,
                                 body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new MulticastMessage
                                 {
                                     Tokens = tokens.Select(t => t.Token).ToList(),
                                     Data = connectionNotifications.FirstOrDefault()?.ToNotificationDTO().ToStringDictionary(),
                                     Notification = new FirebaseAdmin.Messaging.Notification()
                                     {
                                         Title = "New connection request",
                                         Body = connectionNotifications.FirstOrDefault()?.Content,
                                     },
                                 })));
                    break;
                case ResourceNotificationType.MESSAGE:
                    List<MessageNotification> messagenotifications = [];
                    foreach (var token in tokens)
                    {
                        MessageNotification messagenotification = new()
                        {
                            Reciever = await _databaseContext.Users.FindAsync(token.UserId) ?? throw new Exception("Something wrong when getting user!"),
                            Content = content,
                            Message = await _databaseContext.Messages.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting message!")
                        };
                        messagenotifications.Add(messagenotification);
                    }
                    await _databaseContext.MessageNotifications.AddRangeAsync(messagenotifications);
                    await _databaseContext.SaveChangesAsync();
                    model.BasicPublish("NotificationExchange",
                                 string.Empty,
                                 basicProperties: null,
                                 body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new MulticastMessage
                                 {
                                     Tokens = tokens.Select(t => t.Token).ToList(),
                                     Notification = new FirebaseAdmin.Messaging.Notification()
                                     {
                                         Title = "New Message",
                                         Body = messagenotifications.FirstOrDefault()?.Content,
                                     },
                                     Data = messagenotifications.FirstOrDefault()?.ToNotificationDTO().ToStringDictionary(),
                                 })));
                    break;
                case ResourceNotificationType.JOBPOST:
                    List<JobPostNotification> JobPostNotifications = [];
                    foreach (var token in tokens)
                    {
                        JobPostNotification JobPostNotification = new()
                        {
                            Reciever = await _databaseContext.Users.FindAsync(token.UserId) ?? throw new Exception("Something wrong when getting user!"),
                            Content = content,
                            JobPost = await _databaseContext.JobPosts.FindAsync(resourceId) ?? throw new Exception("Something wrong when getting message!")
                        };
                        JobPostNotifications.Add(JobPostNotification);
                    }
                    await _databaseContext.JobPostNotifications.AddRangeAsync(JobPostNotifications);
                    await _databaseContext.SaveChangesAsync();
                    model.BasicPublish("NotificationExchange",
                                 string.Empty,
                                 basicProperties: null,
                                 body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new MulticastMessage
                                 {
                                     Tokens = tokens.Select(t => t.Token).ToList(),
                                     Notification = new FirebaseAdmin.Messaging.Notification()
                                     {
                                         Title = "New job opportunities",
                                         Body = JobPostNotifications.FirstOrDefault()?.Content
                                     },
                                     Data = JobPostNotifications.FirstOrDefault()?.ToNotificationDTO().ToStringDictionary(),
                                 })));
                    break;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}