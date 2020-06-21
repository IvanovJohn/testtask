namespace PipelinesApp.Api.Notification
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public interface INotificationService
    {
        Task SendNotification(string message);
    }

    internal class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationsHub> notificationsHub;

        public NotificationService(IHubContext<NotificationsHub> notificationsHub)
        {
            this.notificationsHub = notificationsHub;
        }

        public async Task SendNotification(string message)
        {
            await this.notificationsHub.Clients.All.SendAsync("Notification", message);
        }
    }
}