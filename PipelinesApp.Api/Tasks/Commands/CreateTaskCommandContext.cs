namespace PipelinesApp.Api.Tasks.Commands
{
    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Tasks.Forms;

    public class CreateTaskCommandContext : AuthenticatedCommandContext
    {
        public CreateTaskForm Form { get; set; }
    }
}