namespace JobNet.DTOs;

public class PostDTO
{
    public required ListUserDTO User { get; set; }
    public int Id { get; set; }
    public int? GroupId { get; set; }
    public required string HTMLContent { get; set; }
    public IList<string> Images { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}