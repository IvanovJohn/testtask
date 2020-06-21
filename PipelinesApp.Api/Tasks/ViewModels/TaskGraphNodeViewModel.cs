namespace PipelinesApp.Api.Pipelines.ViewModels
{
    using PipelinesApp.Api.Tasks.ViewModels;

    public class TaskGraphNodeViewModel
    {
        public TaskViewModel Task { get; set; }

        public string PreviosTaskId { get; set; }
    }
}