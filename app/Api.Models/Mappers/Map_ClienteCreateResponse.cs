
namespace Api.Models.Mappers
{
    public class Map_ClienteCreateResponse
    {
        public static CreateResponse Map(CreateRequest cliente)
        {
            return new CreateResponse
            {
                nome = cliente.nome,
                email = cliente.email,
                agencia = cliente.agencia,
                conta = cliente.conta,
                dac = cliente.dac
            };
        }
    }
}
