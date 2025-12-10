using Api.Models.ContaCorrente;

namespace Api.Models.Mappers
{
    public class Map_SaqueResponse
    {
        public static SaqueResponse Map(SaqueRequest request)
        {
            return new SaqueResponse
            {
                agencia = request.agencia,
                conta = request.conta,
                dac = request.dac,
                valor = request.valor
            };
        }
        public static SaqueResponse Map(EfetivarSaqueRequest request)
        {
            return new SaqueResponse
            {
                agencia = request.agencia,
                conta = request.conta,
                dac = request.dac,
                valor = request.valor,
                token = request.token
            };
        }
    }
}
