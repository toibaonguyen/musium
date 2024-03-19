using System;
using JobNet.Enums;
using MongoDB.Bson;
namespace JobNet.Models.Entities;
public class JobPost
{
    public ObjectId? Id { get; set; }
    public required string JobTitle { get; set; }
    public required ObjectId Author { get; set; }
    public required ObjectId Company { get; set; }
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