
namespace JobNet.DTOs;

public class ExperienceDTO
{
    public required string Title { get; set; }
    public required string EmploymentType { get; set; }
    public required string Location { get; set; }
    public required string LocationType { get; set; }
    public required string Description { get; set; }
    public bool IsUserCurentlyWorking { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

