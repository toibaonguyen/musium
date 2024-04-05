using System;
namespace JobNet.Models.Entities;
public class Comment : Entity
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}