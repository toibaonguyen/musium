
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;
public class CreatePostCommentDTO
{
    [Required]
    public required string Content { get; set; }
    [Required]
    public IList<IFormFile> Images { get; set; } = [];
}