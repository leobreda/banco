namespace Api.Models
{
    public class ClienteCreateRequest
    {
        public string nome { get; set; }
        public string email { get; set; }
        public uint agencia { get; set; }
        public uint conta { get; set; }
        public uint dac { get; set; }

        public ClienteCreateRequest()
        {
            nome = string.Empty;
            email = string.Empty;
            agencia = 0;
            conta = 0;
            dac = 0;
        }
    }
}
