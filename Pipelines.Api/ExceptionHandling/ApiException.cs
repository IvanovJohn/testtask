namespace Pipelines.Api.ExceptionHandling
{
    using System;
    using System.Net;

    /// <summary>
    /// Exception of api.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        protected ApiException(string message, Exception innerException = null)
            : base(message, innerException)
        {
            this.DateTime = System.DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets httpStatusCode code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets error code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets datetime.
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Operation Failed. General Error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <returns>
        /// The <see cref="ApiException"/>.
        /// </returns>
        public static ApiException Error(string message, Exception innerException = null)
        {
            return new ApiException(message, innerException)
                       {
                           Code = "Error",
                           HttpStatusCode = HttpStatusCode.BadRequest,
                           Description = "Operation Failed. General Error.",
                       };
        }

        /// <summary>
        /// Operation Failed. The input data did not pass validation.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <returns>
        /// The <see cref="ApiException"/>.
        /// </returns>
        public static ApiException ErrorInvalidInputData(string message, Exception innerException = null)
        {
            return new ApiException(message, innerException)
                       {
                           Code = "ErrorInvalidInputData",
                           HttpStatusCode = HttpStatusCode.BadRequest,
                           Description = "Operation Failed. The input data is not valid.",
                       };
        }

        /// <summary>
        /// Operation Failed. The authentication data is incorrect.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <returns>
        /// The <see cref="ApiException"/>.
        /// </returns>
        public static ApiException ErrorAuthentication(string message, Exception innerException = null)
        {
            return new ApiException(message, innerException)
                       {
                           Code = "ErrorAuthentication",
                           HttpStatusCode = HttpStatusCode.Forbidden,
                           Description = "Operation Failed. The authentication data is incorrect.",
                       };
        }

        /// <summary>
        /// Operation Failed. The authorization data is incorrect.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <returns>
        /// The <see cref="ApiException"/>.
        /// </returns>
        public static ApiException ErrorAuthorization(string message, Exception innerException = null)
        {
            return new ApiException(message, innerException)
                       {
                           Code = "ErrorAuthorization",
                           HttpStatusCode = HttpStatusCode.Unauthorized,
                           Description = "Operation Failed. The authorization data is incorrect.",
                       };
        }

        /// <summary>
        /// Operation Failed. Item Not Found.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <returns>
        /// The <see cref="ApiException"/>.
        /// </returns>
        public static ApiException ErrorItemNotFound(string message, Exception innerException = null)
        {
            return new ApiException(message, innerException)
                       {
                           Code = "ErrorItemNotFound",
                           HttpStatusCode = HttpStatusCode.NotFound,
                           Description = "Operation Failed. Item Not Found.",
                       };
        }

        /// <summary>
        /// Operation Failed. Duplicate Item Constraint .
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <returns>
        /// The <see cref="ApiException"/>.
        /// </returns>
        public static ApiException ErrorDuplicateItem(string message, Exception innerException = null)
        {
            return new ApiException(message, innerException)
                       {
                           Code = "ErrorDuplicateItem",
                           HttpStatusCode = HttpStatusCode.Conflict,
                           Description = "Operation Failed. Duplicate Item Constraint.",
                       };
        }

        /// <summary>
        /// Operation Failed. Internal error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        /// <returns>
        /// The <see cref="ApiException"/>.
        /// </returns>
        public static ApiException InternalError(string message, Exception innerException = null)
        {
            return new ApiException(message, innerException)
                       {
                           Code = "InternalError",
                           HttpStatusCode = HttpStatusCode.InternalServerError,
                           Description = "Operation Failed. Internal error.",
                       };
        }
    }
}