using System;
namespace JobNet.Models.Entities;
public class Group
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public int AdminId { get; set; }
    public User Admin { get; set; } = null!;
    public required string Description { get; set; }
    public int IndustryId { get; set; }
    public required Industry Industry { get; set; } = null!;
    public ICollection<User> Users { get; } = new List<User>();
    public ICollection<Post> Posts { get; } = new List<Post>();
    public required string Location { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required bool IsActive { get; set; }
}
