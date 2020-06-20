namespace PipelinesApp.Api.Pipelines.Queries
{
    using PipelinesApp.Api.Core.Queries;

    internal class PipelineByIdCriterion : ICriterion
    {
        public string Id { get; set; }
    }
}