namespace Pipelines.Api.Core.Queries
{
    using System;

    /// <summary>
    /// Exception is used when QueryHandler is not found.
    /// </summary>
    public class QueryHandlerNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryHandlerNotFoundException"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public QueryHandlerNotFoundException(Type type)
        {
        }
    }
}