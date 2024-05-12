using JobNet.Contants;
using JobNet.DTOs;
using JobNet.Interfaces.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace JobNet.Hubs;

[Authorize(Policy = IdentityData.UserPolicyName)]
public sealed class PrivateChatHub : Hub<IPrivateChatListenerHub>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}