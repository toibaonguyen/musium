using System.ComponentModel.DataAnnotations;
namespace JobNet.DTOs;

public class CreateJobPostDTO
{
    [Required]
    public required string JobTitle { get; set; }
    [Required]
    public required IList<string> WorkplaceTypes { get; set; }
    [Required]
    public required IList<string> JobTypes { get; set; }
    [Required]
    public required string JobDescription { get; set; }
    [Required]
    public required IList<string> Skills { get; set; } = [];
    [Required]
    public required IList<string> JobRequirements { get; set; } = [];
    [Required]
    public required string ContactInfo { get; set; }
    [Required]
    public required string JobLocation { get; set; }
    [Required]
    public required DateTime ExpiredAt { get; set; }
    [Required]
    public required bool IsActive { get; set; }

}