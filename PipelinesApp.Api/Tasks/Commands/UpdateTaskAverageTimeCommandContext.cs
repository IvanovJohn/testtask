namespace PipelinesApp.Api.Tasks.Commands
{
    using PipelinesApp.Api.Core.Commands;

    public class UpdateTaskAverageTimeCommandContext : ICommandContext
    {
        public string Id { get; set; }

        public long LastDuration { get; set; }
    }
}