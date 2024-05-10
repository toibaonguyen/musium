
namespace JobNet.DTOs;

public class MessageDTO
{
    public required int SenderId { get; set; }
    public required int ConversationId { get; set; }
    public required string Content { get; set; }
    public string? ImageURL { get; set; }
    public string? VideoURL { get; set; }
    public string? OtherFileURL { get; set; }
    public required DateTime SentAt { get; set; }
}

