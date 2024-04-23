using System;
using JobNet.Contants;
using Microsoft.EntityFrameworkCore;
namespace JobNet.Models.Entities;
public class Post : Entity
{
    public int Id { get; set; }
    [DoNotPatch]
    public int OwnerId { get; set; }
    [DoNotPatch]
    public User Owner { get; set; } = null!;
    [DoNotPatch]
    public int? GroupId { get; set; }
    [DoNotPatch]
    public Group? Group { get; set; }
    public required string Content { get; set; }
    public IList<string> Images { get; set; } = [];
    public ICollection<Comment> Comments { get; } = [];
    [DoNotPatch]
    public DateTime CreatedAt { get; set; }
    [DoNotPatch]
    public DateTime UpdatedAt { get; set; }
    [DoNotPatch]
    public BannedPost? BannedPost { get; set; }
    public required bool IsActive { get; set; }
}