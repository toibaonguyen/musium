namespace JobNet.DTOs;

public class ListCompanyDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required int NumberOfFollowers { get; set; }
    public required string Headquarters { get; set; }
}