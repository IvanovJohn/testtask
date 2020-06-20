namespace PipelinesApp.Api.Pipelines.ViewModels
{
    using System.Collections.Generic;

    using PipelinesApp.Api.Tasks.ViewModels;

    public class PipelineDetailsViewModel : PipelineViewModel
    {
        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}