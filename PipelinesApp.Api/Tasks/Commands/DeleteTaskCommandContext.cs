namespace PipelinesApp.Api.Tasks.Commands
{
    using PipelinesApp.Api.Core.Commands;

    public class DeleteTaskCommandContext : AuthenticatedCommandContext
    {
        public string Id { get; set; }
    }
}