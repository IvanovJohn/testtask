namespace Pipelines.Api.Core.Queries
{
    using System;
    using System.Threading.Tasks;

    public interface IQueryDispatcher
    {
        Task<TResult> Ask<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }

    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> Ask<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = (IQueryHandler<TQuery, TResult>)this.serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

            if (handler == null)
            {
                throw new QueryHandlerNotFoundException(typeof(IQuery<TResult>));
            }

            return await handler.Ask(query);
        }
    }
}