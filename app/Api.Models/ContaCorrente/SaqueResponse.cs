namespace Api.Models.ContaCorrente
{
    public class SaqueResponse : ContaCorrente
    {
        public decimal valor { get; set; }

        public string token { get; set; }

        public SaqueResponse()
        {
            valor = 0;
            token = string.Empty;
        }
    }
}