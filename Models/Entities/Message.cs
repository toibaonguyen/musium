using System;
namespace JobNet.Models.Entities;
public class Message
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public User Sender { get; set; } = null!;
    public int ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsImage { get; set; }
}
