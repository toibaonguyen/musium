using System;
namespace JobNet.Models.Entities;
public class UserInConversation : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;
}