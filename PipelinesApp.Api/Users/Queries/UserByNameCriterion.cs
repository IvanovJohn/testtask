namespace PipelinesApp.Api.Users.Queries
{
    using PipelinesApp.Api.Core.Queries;

    public class UserByNameCriterion : ICriterion
    {
        public string Name { get; set; }
    }
}