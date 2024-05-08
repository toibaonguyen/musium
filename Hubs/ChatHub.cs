using JobNet.DTOs;
using JobNet.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace JobNet.Hubs;

public sealed class ChatHub : Hub
{
    // public Task SendMessage(int senderId, int recieverId, MessageDTO message)
    // {
    //     throw new NotImplementedException();
    // }
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("RecieveMessage", $"{Context.ConnectionId} has connected");
    }
}