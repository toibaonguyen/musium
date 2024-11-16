using JobNet.Enums;

namespace JobNet.Models.Entities;

public class PostReaction : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public PostReactionType React { get; set; }
    public DateTime CreatedAt { get; set; }
}