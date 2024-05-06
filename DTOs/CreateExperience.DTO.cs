using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class CreateExperienceDTO
{
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string EmploymentType { get; set; }
    [Required]
    public required string Location { get; set; }
    [Required]
    public required string LocationType { get; set; }
    [Required]
    public required string Description { get; set; }
    [Required]
    public bool IsUserCurentlyWorking { get; set; }
    [Required]
    public required DateTime StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
}