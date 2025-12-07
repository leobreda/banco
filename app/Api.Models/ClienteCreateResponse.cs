namespace Api.Models
{
    public class ClienteCreateResponse : ClienteCreateRequest
    {
        public int id { get; set; }
        public string senha { get; set; }

        public ClienteCreateResponse()
        {
            id = 0;
            senha = string.Empty;
        }

    }
}
