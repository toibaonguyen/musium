using System;
using JobNet.Enums;
namespace JobNet.Models.Entities;
public class JobPost : Entity
{
    public int Id { get; set; }
    public required string JobTitle { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public JobPostBill? Bill { get; set; }
    public required LocationType WorkplaceType { get; set; }
    public required string JobLocation { get; set; }
    public required EmploymentType JobType { get; set; }
    public required string Description { get; set; }
    public ICollection<JobPostSkill> JobPostSkills { get; } = [];
    public required string ReceiveApplicantsEmail { get; set; }
    public required DateTime ExpiredAt { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }

}