namespace Api.Models.Mappers
{
    public class Map_Cliente
    {
        public static Cliente Map(ClienteCreateRequest cliente)
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
        public static Cliente Map(uint id, ClienteUpdateRequest cliente)
        {
            return new Cliente
            {
                id = id,
                nome = cliente.nome,
                email = cliente.email,
            };
        }
    }
}