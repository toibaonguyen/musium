
namespace JobNet.DTOs;

public class MessageDTO
{
    public required string Content { get; set; }
    public IFormFile? Image { get; set; }
    public IFormFile? Video { get; set; }
    public IFormFile? OtherFile { get; set; }
}

