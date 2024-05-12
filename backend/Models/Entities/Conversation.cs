
using JobNet.Enums;

namespace JobNet.Models.Entities;

public class Conversation : Entity
{
    public int Id { get; set; }
    public ICollection<UserInConversation> Users { get; } = [];
    public ICollection<Message> Messages { get; } = [];
}