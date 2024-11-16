using System;
namespace JobNet.Models.Entities;
public class Company : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string BackgroundImage { get; set; }
    public required string Description { get; set; }
    public string? Website { get; set; }
    public required int CompanySize { get; set; }
    public required string Headquarters { get; set; }
    public required DateTime FoundedAt { get; set; }
    public ICollection<UserFollowCompany> Followers { get; } = [];
    public ICollection<Experience> UserExperiences { get; } = [];
    public ICollection<JobPost> JobPosts { get; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}
