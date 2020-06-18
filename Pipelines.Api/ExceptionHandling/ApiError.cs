namespace Pipelines.Api.ExceptionHandling
{
    using System;

    /// <summary>
    /// Api error.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Gets or sets api error code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets error description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets server datetime.
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Gets or sets inner error.
        /// </summary>
        public ApiError InnerError { get; set; }
    }
}