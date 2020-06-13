﻿namespace Pipelines.Api.Core
{
    using System.Threading.Tasks;

    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Ask(TQuery query);
    }
}