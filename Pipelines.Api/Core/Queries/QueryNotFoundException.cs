namespace Pipelines.Api.Core.Queries
{
    using System;

    /// <summary>
    /// Exception is used when QueryHandler is not found.
    /// </summary>
    public class QueryNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryNotFoundException"/> class.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public QueryNotFoundException(Type type)
        {
        }
    }
}