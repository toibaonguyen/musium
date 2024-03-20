using System;
namespace JobNet.Models.Entities;
public class Comment
{
    public int Id { get; set; }
    public required Post Post { get; set; }
    public required User Author { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}