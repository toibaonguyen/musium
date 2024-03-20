using System;
namespace JobNet.Models.Entities;
public class Message
{
    public int Id { get; set; }
    public required User Sender { get; set; }
    public required User Receiver { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsImage { get; set; }
}
