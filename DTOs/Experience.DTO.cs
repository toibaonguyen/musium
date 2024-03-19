using JobNet.Models.Entities;
using MongoDB.Bson;

namespace JobNet.DTOs;

public class ExperienceDTO
{
    public required ObjectId Id { get; set; }
    public required string Title { get; set; }
    public required string EmploymentType { get; set; }
    public required string Location { get; set; }
    public required string LocationType { get; set; }
    public required string Description { get; set; }
    public bool IsUserWorkingAt { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}

