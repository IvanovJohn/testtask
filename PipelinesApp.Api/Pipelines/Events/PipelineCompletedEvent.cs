namespace PipelinesApp.Api.Pipelines.Events
{
    using PipelinesApp.Api.Core.Events;
    using PipelinesApp.Api.Pipelines.ViewModels;

    public class PipelineCompletedEvent : IEvent
    {
        public PipelineDetailsViewModel Pipeline { get; set; }
    }
}