
using JobNet.DTOs;

namespace JobNet.Interfaces.Hubs;

public interface IChatHub
{
    Task SendMessage(int senderId, int recieverId, MessageDTO message);
}