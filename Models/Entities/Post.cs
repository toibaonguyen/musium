
using JobNet.Contants;
namespace JobNet.Models.Entities;
public class Post : Entity
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public required string Content { get; set; }
    public IList<string> Images { get; set; } = [];
    public IList<string> Videos { get; set; } = [];
    public IList<string> OtherFiles { get; set; } = [];
    public ICollection<Comment> Comments { get; } = [];
    public ICollection<PostReaction> Reactions { get; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }
    public ICollection<PostNotification> Notifications { get; set; } = [];
}