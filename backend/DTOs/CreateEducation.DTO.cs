

namespace JobNet.DTOs;

public class CreateEducationDTO
{
    public required string SchoolName { get; set; }
    public required string Degree { get; set; }
    public required string Major { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Grade { get; set; }
    public string? ActivitiesAndSocieties { get; set; }
    public string? Description { get; set; }
}