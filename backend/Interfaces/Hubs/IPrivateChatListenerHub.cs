
using JobNet.DTOs;

namespace JobNet.Interfaces.Hubs;
public interface IPrivateChatListenerHub
{
    Task RecieveMessage(MessageDTO message);
}