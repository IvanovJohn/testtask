namespace PipelinesApp.Api.ExceptionHandling
{
    using System;
    using System.Net;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Api exception handling.
    /// </summary>
    internal class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            ApiErrorResponse resultObject = null;
            var apiException = this.GetApiException(context.Exception);

            if (apiException != null)
            {
                resultObject = this.GetResultByException(apiException);
                context.HttpContext.Response.StatusCode = (int)apiException.HttpStatusCode;
            }


            if (resultObject == null)
            {
                resultObject = this.GetResultByException(context.Exception);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            resultObject.RequestId = context.HttpContext.Request.Headers["RequestId"];
            context.Result = new ObjectResult(resultObject);
        }

        private ApiException GetApiException(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            if (exception is ApiException)
            {
                return (ApiException)exception;
            }

            if (exception.InnerException == null)
            {
                return null;
            }

            return this.GetApiException(exception.InnerException);
        }

        private ApiErrorResponse GetResultByException(Exception exception)
        {
            var resultObject = new ApiErrorResponse();

            resultObject.Error = this.GetApiError(exception);
            return resultObject;
        }

        private ApiError GetApiError(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            var result = new ApiError();
            result.Message = exception.Message;

            if (exception is ApiException apiException)
            {
                result.Code = apiException.Code;
                result.Description = apiException.Description;
            }

            result.InnerError = this.GetApiError(exception.InnerException);
            return result;
        }
    }
}