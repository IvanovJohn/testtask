namespace Pipelines.Api.Users.Queries
{
    using Pipelines.Api.Core.Queries;

    public class UserByNameCriterion : ICriterion
    {
        public string Name { get; set; }
    }
}