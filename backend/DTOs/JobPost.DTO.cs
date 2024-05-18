namespace JobNet.DTOs;

public class JobPostDTO
{
    public required int Id { get; set; }
    public required string JobTitle { get; set; }
    public required IList<string> WorkplaceType { get; set; }
    public required IList<string> JobType { get; set; }
    public required string JobDescription { get; set; }
    public required IList<string> Skills { get; set; } = [];
    public required IList<string> JobRequirements { get; set; } = [];
    public required string ContactInfo { get; set; }
    public required string JobLocation { get; set; }
    public required DateTime ExpiredAt { get; set; }
}