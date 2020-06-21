namespace PipelinesApp.Api.Tasks.Events
{
    using PipelinesApp.Api.Core.Events;

    public class TaskCompletedEvent : IEvent
    {
        public string TaskId { get; set; }

        public long Duration { get; set; }
    }
}