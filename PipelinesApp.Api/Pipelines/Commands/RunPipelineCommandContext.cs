namespace PipelinesApp.Api.Pipelines.Commands
{
    using PipelinesApp.Api.Core.Commands;

    public class RunPipelineCommandContext : ICommandContext
    {
        public string Id { get; set; }
    }
}