namespace JobNet.DTOs;

public class PostDTO
{
    public required ListUserDTO User { get; set; }
    public int Id { get; set; }
    public required string Content { get; set; }
    public IList<string> Images { get; set; } = [];
    public IList<string> Videos { get; set; } = [];
    public IList<string> OtherFiles { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}