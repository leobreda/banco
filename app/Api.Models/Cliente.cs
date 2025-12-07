namespace Api.Models
{
    public class Cliente
    {
        public uint id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public uint agencia { get; set; }
        public uint conta { get; set; }
        public uint dac { get; set; }
        public string senha { get; set; }
        public string token { get; set; }
        public float saldo { get; set; }



        public Cliente()
        {
            id = 0;
            nome = string.Empty;
            email = string.Empty;
            agencia = 0;
            conta = 0;
            dac = 0;
            senha = string.Empty;
            token = string.Empty;
            saldo = 0;
        }


    }
}
