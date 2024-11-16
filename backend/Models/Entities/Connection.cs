
using JobNet.Enums;

namespace JobNet.Models.Entities;

public class Connection : Entity
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public User Sender { get; set; } = null!;
    public int RecieverId { get; set; }
    public User Reciever { get; set; } = null!;
    public ConnectionRequestStatusType Status { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ConnectionRequestNotification? Notification { get; set; }
}