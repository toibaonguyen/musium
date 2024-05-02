
using System.ComponentModel.DataAnnotations;

namespace JobNet.DTOs;

public class CreatePostDTO
{
    [Required]
    public required string Content { get; set; }
    [Required]
    public IList<IFormFile> Images { get; set; } = [];
    [Required]
    public IList<IFormFile> Videos { get; set; } = [];
    [Required]
    public IList<IFormFile> OtherFiles { get; set; } = [];
}