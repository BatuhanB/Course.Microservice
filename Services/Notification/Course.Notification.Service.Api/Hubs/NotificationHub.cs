using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Course.Notification.Service.Api.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public async Task SendMessage(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveMessage", message);
    }
}
