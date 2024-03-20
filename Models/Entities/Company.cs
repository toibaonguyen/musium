using System;
namespace JobNet.Models.Entities;
public class Company
{
    public int Id { get; set; }
    public required User Manager { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public string? BackgroundImage { get; set; }
    public required string Description { get; set; }
    public string? Website { get; set; }
    public required Industry Industry { get; set; }
    public required int CompanySize { get; set; }
    public required string Headquarters { get; set; }
    public required DateTime FoundedAt { get; set; }
    public required ICollection<Post> Posts { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime IsActive { get; set; }
}
