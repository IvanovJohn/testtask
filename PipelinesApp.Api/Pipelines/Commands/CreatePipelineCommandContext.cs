namespace PipelinesApp.Api.Pipelines.Commands
{
    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Pipelines.Forms;

    public class CreatePipelineCommandContext : AuthenticatedCommandContext
    {
        public CreatePipelineForm Form { get; set; }
    }
}