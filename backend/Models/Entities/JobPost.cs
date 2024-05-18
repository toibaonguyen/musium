using System;
using JobNet.Enums;
namespace JobNet.Models.Entities;
public class JobPost : Entity
{
    public int Id { get; set; }
    public required string JobTitle { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public IList<LocationType> WorkplaceTypes { get; set; } = [];
    public IList<EmploymentType> JobTypes { get; set; } = [];
    public required string JobDescription { get; set; }
    public ICollection<JobPostSkill> JobPostSkills { get; } = [];
    public ICollection<JobPostNotification> JobPostNotifications { get; } = [];
    public IList<string> JobRequirements { get; set; } = [];
    public required string ContactInfo { get; set; }
    public required string JobLocation { get; set; }
    public required DateTime ExpiredAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }

}