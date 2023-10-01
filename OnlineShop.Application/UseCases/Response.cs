namespace OnlineShop.Application.UseCase
{ 
    public class Response
    {
        public string Status { get; set; }
        public object Result { get; set; }
        public Response() { }
        public Response(string status, object data)
        {
            Status = status;
            Result = data;
        }
    }


}
