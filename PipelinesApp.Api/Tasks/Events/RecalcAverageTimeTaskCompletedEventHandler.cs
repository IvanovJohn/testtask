namespace PipelinesApp.Api.Tasks.Events
{
    using System.Threading.Tasks;

    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Core.Events;
    using PipelinesApp.Api.Tasks.Commands;

    internal class RecalcAverageTimeTaskCompletedEventHandler : IEventHandler<TaskCompletedEvent>
    {
        private readonly ICommandDispatcher commandDispatcher;

        public RecalcAverageTimeTaskCompletedEventHandler(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public async Task Handle(TaskCompletedEvent domainEvent)
        {
            await this.commandDispatcher.Execute(
                new UpdateTaskAverageTimeCommandContext
                    {
                        Id = domainEvent.TaskId, LastDuration = domainEvent.Duration
                    });
        }
    }
}