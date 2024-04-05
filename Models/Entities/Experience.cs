using System;
using JobNet.Enums;
namespace JobNet.Models.Entities;
public class Experience : Entity
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public required User Author { get; set; } = null!;
    public required string Title { get; set; }
    public required EmploymentType EmploymentType { get; set; }
    //Cai nay dang ngo vl
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public required string Location { get; set; }
    public required LocationType LocationType { get; set; }
    public required string Description { get; set; }
    public bool IsUserCurentlyWorking { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
