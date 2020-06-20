namespace PipelinesApp.Api.Tasks.Queries
{
    using PipelinesApp.Api.Core.Queries;

    internal class TaskByIdsCriterion : ICriterion
    {
        public string[] Ids { get; set; }
    }
}