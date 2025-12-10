
namespace Api.Models.Mappers
{
    public class Map_Cliente
    {
        public static Cliente Map(CreateRequest cliente)
        {
            return new Cliente
            {
                nome = cliente.nome,
                email = cliente.email,
                agencia = cliente.agencia,
                conta = cliente.conta,
                dac = cliente.dac
            };
        }
        public static Cliente Map(uint id, UpdateRequest cliente)
        {
            return new Cliente
            {
                id = id,
                nome = cliente.nome,
                email = cliente.email,
            };
        }
        public static Cliente Map(LoginRequest request)
        {
            return new Cliente
            {
                agencia = request.agencia,
                conta = request.conta,
                dac = request.dac,
                senha = request.senha
            };
        }

        
    }
}