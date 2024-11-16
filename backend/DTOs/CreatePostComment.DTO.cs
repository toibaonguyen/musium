
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;
public class CreatePostCommentDTO
{
    [Required]
    public required string Content { get; set; }
    public IFormFile? Image { get; set; }
}