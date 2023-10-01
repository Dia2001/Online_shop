using Microsoft.IdentityModel.Tokens;
using OnlineShop.Core.ExceptionHandle;
using OnlineShop.Utils;
using System.Net;

namespace OnlineShop.Application.Helpers
{
    public class MiddlewareHelper
    {
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string message;
            string? title;
            int statusCode = (int)HttpStatusCode.InternalServerError;
            Object? errorDetail = null;

            switch (exception)
            {
                case MyCustomException:
                    var myCustomException = (MyCustomException)exception;
                    title = myCustomException.Title;
                    message = myCustomException.Message;
                    statusCode = (int)myCustomException.StatusCode;
                    errorDetail = myCustomException.Details;
                    context.Response.Headers.Add(ExceptionUtil.ResponseHeaders.TOKEN_EXPIRED, "false");
                    break;

                case SecurityTokenExpiredException:
                    title = "EXCEPTION.TOKEN_EXPIRED";
                    message = "EXCEPTION.PLEASE_RELOGIN";
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.Headers.Add(ExceptionUtil.ResponseHeaders.TOKEN_EXPIRED, "true");
                    break;

                default:
                    title = "EXCEPTION.AN_ERROR_OCCURRED";
                    message = "EXCEPTION.PLEASE_CONTACT_YOUR_SYSTEM_ADMINISTRATOR";
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorDetail = new
                    {
                        Source = "Core Base",
                        ExceptionMessage = exception.Message,
                        InnerExceptionMessage = exception.InnerException != null ? exception.InnerException.Message : "",
                        StackTrace = exception.StackTrace
                    };
                    context.Response.Headers.Add(ExceptionUtil.ResponseHeaders.TOKEN_EXPIRED, "false");
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            message = string.IsNullOrEmpty(message) ? null : message;
            title = string.IsNullOrEmpty(title) ? null : title;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                Title = title,
                Message = message,
                Details = errorDetail
            }.ToString());
        }
    }
}
