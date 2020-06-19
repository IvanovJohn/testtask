namespace PipelinesApp.Api.Core.Queries
{
    using System;
    using System.Threading.Tasks;

    public interface IQueryDispatcher
    {
        Task<TResult> Ask<TQuery, TResult>(TQuery criterion)
            where TQuery : ICriterion;
    }

    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> Ask<TCriterion, TResult>(TCriterion criterion)
            where TCriterion : ICriterion
        {
            if (criterion == null)
            {
                throw new ArgumentNullException(nameof(criterion));
            }

            var query = (IQuery<TCriterion, TResult>)this.serviceProvider.GetService(typeof(IQuery<TCriterion, TResult>));

            if (query == null)
            {
                throw new QueryNotFoundException(typeof(IQuery<TCriterion, TResult>));
            }

            return await query.Ask(criterion);
        }
    }
}