
using System.Net;

namespace OnlineShop.Services.Base
{
    public class SmsService
    {
        public const int TYPE_QC = 1;
        public const int TYPE_CSKH = 2;
        public const int TYPE_BRANDNAME = 3;
        public const int TYPE_BRANDNAME_NOTIFY = 4; // Gửi sms sử dụng brandname Notify
        public const int TYPE_GATEWAY = 5; // Gửi sms sử dụng app android từ số di động cá nhân, download app tại đây: https://speedsms.vn/sms-gateway-service/

        const string rootURL = "https://api.speedsms.vn/index.php";
        private readonly string accessToken = "";

        public SmsService() { }

        public SmsService(string token)
        {
            accessToken = token;
        }

        public string SendSMS(string[] phones, string content, int type, string sender)
        {
            string url = rootURL + "/sms/send";
            if (phones.Length <= 0)
                return "";
            if (type == TYPE_BRANDNAME && sender.Equals(""))
                return "";

            NetworkCredential myCreds = new(accessToken, ":x");
            WebClient client = new();
            client.Credentials = myCreds;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            string builder = "{\"to\":[";

            for (int i = 0; i < phones.Length; i++)
            {
                builder += "\"" + phones[i] + "\"";
                if (i < phones.Length - 1)
                {
                    builder += ",";
                }
            }
            builder += "], \"content\": \"" + Uri.EscapeDataString(content) + "\", \"type\":" + type + ", \"sender\": \"" + sender + "\"}";

            string json = builder.ToString();
            return client.UploadString(url, json);
        }
    }
}
