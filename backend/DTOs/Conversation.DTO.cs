namespace JobNet.DTOs;

public class ConversationDTO
{
    public required int ConversationId { get; set; }
    public required ChatUserDTO WithUser { get; set; }
    public MessageDTO? LastestMessage { get; set; }
}