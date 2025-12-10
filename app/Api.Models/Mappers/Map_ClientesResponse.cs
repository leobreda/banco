
namespace Api.Models.Mappers
{
    public class Map_ClientesResponse
    {
        public static ClientesResponse Map(Cliente cliente)
        {
            return new ClientesResponse
            {
                id = cliente.id,
                nome = cliente.nome,
                email = cliente.email,
                agencia = cliente.agencia,
                conta = cliente.conta,
                dac = cliente.dac
            };
        }
        public static List<ClientesResponse> Map(List<Cliente> clientes)
        {
            List<ClientesResponse> response = new List<ClientesResponse>();
            foreach (var cliente in clientes)
            {
                response.Add(new ClientesResponse
                {
                    id = cliente.id,
                    nome = cliente.nome,
                    email = cliente.email,
                    agencia = cliente.agencia,
                    conta = cliente.conta,
                    dac = cliente.dac
                });
            }
            return response;
        }
    }
}
