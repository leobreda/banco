namespace Api.Models.ContaCorrente
{
    public class SaqueRequest : ContaCorrente
    {
        public string senha { get; set; }
        public decimal valor { get; set; }

        public SaqueRequest()
        {
            senha = string.Empty;
            valor = 0;
        }
    }
}