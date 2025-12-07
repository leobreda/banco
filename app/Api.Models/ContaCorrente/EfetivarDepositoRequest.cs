namespace Api.Models.ContaCorrente
{
    public class EfetivarDepositoRequest : ContaCorrente
    {
        public decimal valor { get; set; }

        public string token { get; set; }

        public EfetivarDepositoRequest()
        {
            valor = 0;
            token = string.Empty;
        }
    }
}
