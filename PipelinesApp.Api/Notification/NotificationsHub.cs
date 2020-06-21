namespace PipelinesApp.Api.Notification
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class NotificationsHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await this.Clients.All.SendAsync("Notification", message);
        }
    }
}