
namespace JobNet.DTOs;

public class CreatePostDTO
{
    public int? GroupId { get; set; }
    public required string HTMLContent { get; set; }
    public IList<string> Images { get; set; } = new List<string>();
}