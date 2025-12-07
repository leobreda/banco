namespace Api.Models.Mappers
{
    public class Map_ClienteCreateResponse
    {
        public static ClienteCreateResponse Map(ClienteCreateRequest cliente)
        {
            return new ClienteCreateResponse
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
