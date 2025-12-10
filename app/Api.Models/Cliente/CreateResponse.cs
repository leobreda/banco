namespace Api.Models
{
    public class CreateResponse : CreateRequest
    {
        public int id { get; set; }
        public string senha { get; set; }

        public CreateResponse()
        {
            id = 0;
            senha = string.Empty;
        }

    }
}
