
namespace JobNet.Models.Entities;

public abstract class Notification : Entity
{
    public int Id { get; set; }
    public int RecieverId { get; set; }
    public User Reciever { get; set; } = null!;
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ConnectionRequestNotification : Notification
{
    public int ConnectionRequestId { get; set; }
    public Connection Connection { get; set; } = null!;
}

public class PostNotification : Notification
{
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}

public class CompanyPostNotification : Notification
{
    public int CompanyPostId { get; set; }
    public CompanyPost CompanyPost { get; set; } = null!;
}

public class MessageNotification : Notification
{
    public int MessageId { get; set; }
    public Message Message { get; set; } = null!;
}