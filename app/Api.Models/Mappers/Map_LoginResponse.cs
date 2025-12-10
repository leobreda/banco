using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Mappers
{
    public class Map_LoginResponse
    {
        public static LoginResponse Map(Cliente cliente)
        {
            return new LoginResponse
            {
                id = cliente.id,
                nome = cliente.nome,
                email = cliente.email,
                agencia = cliente.agencia,
                conta = cliente.conta,
                dac = cliente.dac,
                token = cliente.token
            };
        }
    }
}
