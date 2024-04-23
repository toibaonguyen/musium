using System;
namespace JobNet.Models.Entities;

public class BannedPost : Entity
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public required string BannedReason { get; set; }
    public DateTime CreatedAt { get; set; }
}