using Api.Models.ContaCorrente;

namespace Api.Models.Mappers
{
    public class Map_DepositoResponse
    {
        public static DepositoResponse Map(DepositarRequest request, string nome)
        {
            return new DepositoResponse
            {
                agencia = request.agencia,
                conta = request.conta,
                dac = request.dac,
                nome = nome,
                valor = request.valor
            };
        }
        public static DepositoResponse Map(EfetivarDepositoRequest request, string nome)
        {
            return new DepositoResponse
            {
                agencia = request.agencia,
                conta = request.conta,
                dac = request.dac,
                nome = nome,
                valor = request.valor
            };
        }
    }
}