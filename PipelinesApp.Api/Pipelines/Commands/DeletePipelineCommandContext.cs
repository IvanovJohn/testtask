namespace PipelinesApp.Api.Pipelines.Commands
{
    using PipelinesApp.Api.Core.Commands;

    public class DeletePipelineCommandContext : AuthenticatedCommandContext
    {
        public string Id { get; set; }
    }
}