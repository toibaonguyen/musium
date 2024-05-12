
namespace JobNet.DTOs;
public class CommentDTO
{
    public required int id;
    public required string Content { get; set; }
    public required string? Image { get; set; }
}