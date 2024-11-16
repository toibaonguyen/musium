


public class CreateCompanyDTO
{
    public required string Name { get; set; }
    public required IFormFile Avatar { get; set; }
    public required IFormFile BackgroundImage { get; set; }
    public required string Description { get; set; }
    public string? Website { get; set; }
    public required int CompanySize { get; set; }
    public required string Headquarters { get; set; }
    public required DateTime FoundedAt { get; set; }
}