namespace Api.Models
{
    public class ClienteDepositarRequest
    {
        public int id_cliente { get; set; }
        public float valor { get; set; }
        public ClienteDepositarRequest()
        {
            id_cliente = 0;
            valor = 0;
        }
    }
}
