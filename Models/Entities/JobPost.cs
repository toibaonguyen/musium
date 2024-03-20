using System;
using JobNet.Enums;
namespace JobNet.Models.Entities;
public class JobPost
{
    public int Id { get; set; }
    public required string JobTitle { get; set; }
    public required User Author { get; set; }
    public required Company Company { get; set; }
    public required LocationType WorkplaceType { get; set; }
    public required string JobLocation { get; set; }
    public required EmploymentType JobType { get; set; }
    public required string Description { get; set; }
    public required string[] SkillKeywords { get; set; }
    public required string ReceiveApplicantsEmail { get; set; }
    public required DateTime ExpiredAt { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public required bool IsActive { get; set; }

}