namespace Api.Models
{
    public class UpdateRequest
    {
        public string nome { get; set; }
        public string email { get; set; }

        public UpdateRequest()
        {
            nome = string.Empty;
            email = string.Empty;
        }
    }
}
