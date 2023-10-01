using System.Text.Json;

namespace OnlineShop.Utils
{
    public class ExceptionUtil
    {
        public struct ResponseHeaders
        {
            public const string TOKEN_EXPIRED = "Token-Expired";
        }
    }

    public class ErrorDetails
    { 
        public string Message { get; set; }
        public string? Title { get; set; }
        public Object? Details { get; set; }

        public ErrorDetails()
        {
            Title = "";
            Message = "";
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
