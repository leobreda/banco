namespace Api.Models.ContaCorrente
{
    public class SaldoResponse : ContaCorrente
    {
        public decimal saldo { get; set; }
        public SaldoResponse()
        {
            saldo = 0;
        }
    }
}
