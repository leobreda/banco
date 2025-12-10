namespace Api.Models.ContaCorrente
{
    public class DepositarRequest : ContaCorrente
    {
        public decimal valor { get; set; }
        public DepositarRequest()
        {
            valor = 0;
        }
    }
}