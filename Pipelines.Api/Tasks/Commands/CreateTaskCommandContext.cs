namespace Pipelines.Api.Tasks.Commands
{
    using Pipelines.Api.Core.Commands;
    using Pipelines.Api.Tasks.Forms;

    public class CreateTaskCommandContext : ICommandContext
    {
        public CreateTaskForm Form { get; set; }
    }
}