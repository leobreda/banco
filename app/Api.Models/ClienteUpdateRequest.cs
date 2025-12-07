namespace Api.Models
{
    public class ClienteUpdateRequest
    {
        public string nome { get; set; }
        public string email { get; set; }

        public ClienteUpdateRequest()
        {
            nome = string.Empty;
            email = string.Empty;
        }
    }
}
