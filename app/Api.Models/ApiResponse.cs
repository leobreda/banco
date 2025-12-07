namespace Api.Models
{
    public class ApiResponse<T>
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public T? data { get; set; }

        public ApiResponse()
        {
            status_code = 200;
            message = "Success";
        }
    }
}
