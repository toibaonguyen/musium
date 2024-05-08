
namespace JobNet.DTOs;

public class CompanyDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required string BackgroundImage { get; set; }
    public required string Description { get; set; }
    public string? Website { get; set; }
    public required int CompanySize { get; set; }
    public required string Headquarters { get; set; }
    public required DateTime FoundedAt { get; set; }
    public required int NumberOfFollowers { get; set; }
}