namespace PipelinesApp.Api.Pipelines.ViewModels
{
    using System.Collections.Generic;

    public class PipelineDetailsViewModel : PipelineViewModel
    {
        public IEnumerable<TaskGraphNodeViewModel> Tasks { get; set; }
    }
}