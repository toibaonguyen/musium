
namespace JobNet.DTOs;

public class NotificationDTO
{
    public required string NavigationType { get; set; }
    public required int ResourceId { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}

