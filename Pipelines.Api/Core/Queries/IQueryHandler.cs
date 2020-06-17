namespace Pipelines.Api.Core.Queries
{
    using System.Threading.Tasks;

    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Ask(TQuery query);
    }
}