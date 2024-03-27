using System;
namespace JobNet.Models.Entities;
public class Post
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public int? GroupId { get; set; }
    public Group? Group { get; set; }
    public required string Content { get; set; }
    public required IList<string> Images { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }
}