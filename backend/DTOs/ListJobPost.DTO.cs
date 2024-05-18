namespace JobNet.DTOs;

public class ListJobPostDTO
{
    public required int Id { get; set; }
    public required string JobTitle { get; set; }
    public required string CompanyName { get; set; }
    public required string CompanyAvatar { get; set; }
    public required string JobLocation { get; set; }
    public required IList<string> WorkplaceType { get; set; }
}