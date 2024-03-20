using System;
using JobNet.Enums;
namespace JobNet.Models.Entities;
public class Experience
{
    public int Id { get; set; }
    public required User Author { get; set; }
    public required string Title { get; set; }
    public required EmploymentType EmploymentType { get; set; }
    public required Company Company { get; set; }
    public required string Location { get; set; }
    public required LocationType LocationType { get; set; }
    public required string Description { get; set; }
    public bool IsUserWorkingAt { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
