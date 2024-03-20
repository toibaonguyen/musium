using System;
namespace JobNet.Models.Entities;
public class Post
{
    public int Id { get; set; }
    public required User Owner { get; set; }
    public required string Content { get; set; }
    public required IList<string> Images { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }
}