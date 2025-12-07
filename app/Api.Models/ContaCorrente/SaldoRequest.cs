namespace Api.Models.ContaCorrente
{
    public class SaldoRequest : ContaCorrente
    {
        public string senha { get; set; }

        public SaldoRequest()
        {
            senha = string.Empty;
        }
    }
}
