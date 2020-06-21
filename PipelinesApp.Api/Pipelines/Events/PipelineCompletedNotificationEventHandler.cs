namespace PipelinesApp.Api.Pipelines.Events
{
    using System.Threading.Tasks;

    using PipelinesApp.Api.Core.Events;
    using PipelinesApp.Api.Notification;

    internal class PipelineCompletedNotificationEventHandler : IEventHandler<PipelineCompletedEvent>
    {
        private readonly INotificationService notificationService;

        public PipelineCompletedNotificationEventHandler(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task Handle(PipelineCompletedEvent domainEvent)
        {
            await this.notificationService.SendNotification($"Pipeline '{domainEvent.Pipeline.Name}' completed");
        }
    }
}