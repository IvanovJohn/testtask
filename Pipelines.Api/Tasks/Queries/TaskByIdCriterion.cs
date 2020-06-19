namespace Pipelines.Api.Tasks.Queries
{
    using Pipelines.Api.Core.Queries;

    internal class TaskByIdCriterion : ICriterion
    {
        public string Id { get; set; }
    }
}