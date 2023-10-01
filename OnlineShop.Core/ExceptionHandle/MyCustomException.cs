using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.ExceptionHandle
{
    public class NotFoundException : MyCustomException
    {
        public NotFoundException(string? message = "EXCEPTION.NOT_FOUND") : base(message, ExceptionType.NOT_FOUND)
        {
        }
    }

    public class ForbiddenException : MyCustomException
    {
        public ForbiddenException(string? message = "EXCEPTION.FORBIDDEN") : base(message, ExceptionType.NO_PERMISSION)
        {
        }
    }

    public class BadRequestException : MyCustomException
    {
        public BadRequestException(string? message = "EXCEPTION.BAD_REQUEST", string? title = "EXCEPTION.AN_ERROR_OCCURRED") : base(message, title, ExceptionType.BAD_REQUEST)
        {
        }
    }

    public class ObjectExistException : MyCustomException
    {
        public ObjectExistException(string? message = "EXCEPTION.OBJECT_EXIST") : base(message, ExceptionType.BAD_REQUEST)
        {
        }
    }

    public class UnauthorizedException : MyCustomException
    {
        public UnauthorizedException(string? message = "EXCEPTION.UNAUTHORIZED") : base(message, ExceptionType.UNAUTHORIZED)
        {
        }
    }
    public class MyCustomException : System.Exception
    {
        public ExceptionType ExceptionType;
        public HttpStatusCode StatusCode;
        public string? Title;
        public Object? Details;

        public MyCustomException(string? message) : base(message)
        {
            ExceptionType = ExceptionType.DEFAULT;
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public MyCustomException(string? message, HttpStatusCode httpStatusCode) : base(message)
        {
            StatusCode = httpStatusCode;
        }

        public MyCustomException(string? message, string? title, HttpStatusCode httpStatusCode) : base(message)
        {
            Title = title;
            StatusCode = httpStatusCode;
        }

        public MyCustomException(string? message, ExceptionType exceptionType) : this(message, exceptionType, null)
        {

        }

        public MyCustomException(string? message, string? title, ExceptionType exceptionType) : this(message, exceptionType, null, title)
        {
        }

        public MyCustomException(string? message, ExceptionType exceptionType, Object? details, string? title = null) : base(message)
        {
            ExceptionType = exceptionType;
            Details = details;
            Title = title;

            switch (exceptionType)
            {
                case ExceptionType.NOT_FOUND:
                    StatusCode = HttpStatusCode.NotFound;
                    break;

                case ExceptionType.NO_PERMISSION:
                    StatusCode = HttpStatusCode.Forbidden;
                    break;

                case ExceptionType.BAD_REQUEST:
                    StatusCode = HttpStatusCode.BadRequest;
                    break;

                case ExceptionType.UNAUTHORIZED:
                    StatusCode = HttpStatusCode.Unauthorized;
                    break;

                case ExceptionType.DEFAULT:
                default:
                    StatusCode = HttpStatusCode.InternalServerError;
                    break;
            }
        }
    }
}
