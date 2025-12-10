using Api.Models.ContaCorrente;

namespace Api.Models.Mappers
{
    public class Map_SaldoResponse
    {
        public static SaldoResponse Map(SaldoRequest request, decimal saldo = 0)
        {
            return new SaldoResponse
            {
                agencia = request.agencia,
                conta = request.conta,
                dac = request.dac,
                saldo = saldo
            };
        }
    }
}
