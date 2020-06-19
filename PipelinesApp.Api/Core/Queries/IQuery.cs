namespace PipelinesApp.Api.Core.Queries
{
    using System.Threading.Tasks;

    public interface IQuery<in TCriterion, TResult> where TCriterion : ICriterion
    {
        Task<TResult> Ask(TCriterion query);
    }
}