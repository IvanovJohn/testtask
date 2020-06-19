namespace Pipelines.Api.Tasks.Commands
{
    using Pipelines.Api.Core.Commands;

    public class DeleteTaskCommandContext : AuthenticatedCommandContext
    {
        public string Id { get; set; }
    }
}