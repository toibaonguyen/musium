


namespace JobNet.DTOs;

public class ListFollowingCompanyDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Avatar { get; set; }
    public required long NumberFollowers { get; set; }
}