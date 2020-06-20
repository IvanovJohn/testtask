namespace PipelinesApp.Api.Tasks.Queries
{
    using PipelinesApp.Api.Core.Queries;

    public class TasksCriterion : ICriterion
    {
        public string SearchString { get; set; }
    }
}