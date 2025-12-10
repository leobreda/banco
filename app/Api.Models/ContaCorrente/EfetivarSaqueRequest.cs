namespace Api.Models.ContaCorrente
{
    public class EfetivarSaqueRequest : ContaCorrente
    {
        public string senha { get; set; }
        public decimal valor { get; set; }

        public string token { get; set; }

        public EfetivarSaqueRequest()
        {
            senha = string.Empty;
            valor = 0;
            token = string.Empty;
        }
    }
}
