using JobNet.DTOs;

namespace JobNet.Interfaces.Services;

public interface IPrivateChatService
{
    Task SendPrivateMessage(int SenderId, int RecieverId, CreateMessageDTO message);
    Task<bool> CheckIfUserIsInConversation(int userId, int conversationId);
    Task<IList<MessageDTO>> GetMessagesOfConversationOrderBySentTimeDesc(int conversationId, int limit, DateTime cursor);
    Task<IList<ConversationDTO>> GetConversationBoxsOfUserOrderByLastMessageSentTimeDesc(int userId, int limit, DateTime cursor);
}