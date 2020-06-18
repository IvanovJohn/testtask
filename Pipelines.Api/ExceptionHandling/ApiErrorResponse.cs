namespace Pipelines.Api.ExceptionHandling
{
    /// <summary>
    /// Api error.
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// Gets or sets request Id.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets error.
        /// </summary>
        public ApiError Error { get; set; }
    }
}