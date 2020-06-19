namespace PipelinesApp.Api.Tasks.Queries
{
    using PipelinesApp.Api.Core.Queries;

    internal class TaskByIdCriterion : ICriterion
    {
        public string Id { get; set; }
    }
}