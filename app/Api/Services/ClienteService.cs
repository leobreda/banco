using Api.Bootstrap;
using Api.Data;
using Api.Models;
using Api.Models.Mappers;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IClienteService
    {
        Task<ApiResponse<CreateResponse>> Create(CreateRequest clienteRequest);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
        Task<ApiResponse<List<ClientesResponse>>> ListAll();
        Task<ApiResponse<ClientesResponse>> Detail(uint id);
        Task<ApiResponse<UpdateRequest>> Update(uint id, UpdateRequest request);
        Task<ApiResponse<uint>> Delete(uint id);
    }

    public class ClienteService : IClienteService
    {
        private const uint TOKEN_EXPIRES = 5;

        private readonly IClienteData clienteData;
        public ClienteService(IClienteData _clienteData)
        {
            clienteData = _clienteData;
        }

        public async Task<ApiResponse<CreateResponse>> Create(CreateRequest clienteRequest)
        {
            CreateResponse response = Map_ClienteCreateResponse.Map(clienteRequest);


            //Verifica se a conta ja foi cadastrada
            Cliente cliente = await Detail(clienteRequest.agencia, clienteRequest.conta, clienteRequest.dac);
            
            if (cliente is not null)
            {
                return new ApiResponse<CreateResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Conta corrente ja se encontra cadastrada",
                    data = response
                };
            }

            cliente = Map_Cliente.Map(clienteRequest);

            // A senha sempre será [agencia + conta + dac]
            cliente.senha = cliente.agencia.ToString().PadLeft(4, '0')
                + cliente.conta.ToString().PadLeft(5, '0')
                + cliente.dac.ToString().PadLeft(1, '0');

            cliente.saldo = 10000;

            int ret = await clienteData.Create(cliente);

            

            if (ret.Equals(0))
            {
                return new ApiResponse<CreateResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Falha ao cadastrar Cliente",
                    data = response
                };
            }

            response.id = ret;
            response.senha = cliente.senha;
            

            return new ApiResponse<CreateResponse>()
            {
                status_code = (int)HttpStatusCode.Created,
                message = "Cliente criado com sucesso",
                data = response
            };
        }

        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            Cliente cliente = Map_Cliente.Map(request);

            cliente  = await clienteData.Login(cliente);

            if (cliente is null)
            {
                return new ApiResponse<LoginResponse>()
                {
                    status_code = (int)HttpStatusCode.NotFound,
                    message = "Agencia/Conta/Dac e/ou senha incorreta",
                    data = null
                };
            }

            string token = Token.Get(cliente.agencia, cliente.conta, cliente.dac, TOKEN_EXPIRES);

            if (!await clienteData.UpdateToken(cliente, token))
            {

                return new ApiResponse<LoginResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Falha na geracao do token",
                    data = Map_LoginResponse.Map(cliente)
                };

            }

            cliente.token = token;

            return new ApiResponse<LoginResponse>()
            {
                status_code = (int)HttpStatusCode.OK,
                message = "Login realizado com sucesso",
                data = Map_LoginResponse.Map(cliente)
            };
        }


        public async Task<ApiResponse<List<ClientesResponse>>> ListAll()
        {
            List<Cliente> list = await clienteData.ListAll();

            List<ClientesResponse> ret = Map_ClientesResponse.Map(list);

            return new ApiResponse<List<ClientesResponse>>()
            {
                data = ret
            };
        }

        public async Task<ApiResponse<ClientesResponse>> Detail(uint id)
        {
            var cliente = (await clienteData.Search(new Cliente() { id = id }))?
                .FirstOrDefault<Cliente>();

            if(cliente is null)
            {
                return new ApiResponse<ClientesResponse>()
                {
                    status_code = (int)HttpStatusCode.NotFound,
                    message = "Cliente nao encontrado",
                    data = null
                };
            }

            ClientesResponse ret = Map_ClientesResponse.Map(cliente);

            return new ApiResponse<ClientesResponse>()
            {
                data = ret
            };
        }

        public async Task<ApiResponse<UpdateRequest>> Update(uint id, UpdateRequest request)
        {
            Cliente cliente = (await clienteData.Search(new Cliente() { id = id }))?
                .FirstOrDefault<Cliente>();

            if (cliente is null)
            {
                return new ApiResponse<UpdateRequest>()
                {
                    status_code = (int)HttpStatusCode.NotFound,
                    message = "Cliente nao encontrado",
                    data = request
                };
            }

            cliente = Map_Cliente.Map(id,request);

            int ret = await clienteData.Update(cliente);
            if (ret.Equals(0))
            {
                return new ApiResponse<UpdateRequest>()
                {
                    status_code = (int)HttpStatusCode.NotModified,
                    message = "Os dados do cliente nao foram atualizados",
                    data = request
                };
            }

            return new ApiResponse<UpdateRequest>()
            {
                status_code = (int)HttpStatusCode.Accepted,
                message = "Dados do cliente atualizados com sucesso",
                data = request
            };

        }

        public async Task<ApiResponse<uint>> Delete(uint id)
        {
            var cliente = (await clienteData.Search(new Cliente() { id = id }))?
                .FirstOrDefault<Cliente>();

            if (cliente is null)
            {
                return new ApiResponse<uint>()
                {
                    status_code = (int)HttpStatusCode.NotFound,
                    message = "Cliente nao encontrado",
                    data = id
                };
            }

            int ret = await clienteData.Delete(cliente);

            if (ret.Equals(0))
            {
                return new ApiResponse<uint>()
                {
                    status_code = (int)HttpStatusCode.NotAcceptable,
                    message = "O cliente nao pode ser excluido",
                    data = id
                };
            }

            return new ApiResponse<uint>()
            {
                status_code = (int)HttpStatusCode.Accepted,
                message = "O cliente foi excluido com sucesso",
                data = id
            };

        }

        private async Task<Cliente> Detail(uint agencia, uint conta, uint dac)
        {
            return (await clienteData.Search(new Cliente()
            {
                agencia = agencia,
                conta = conta,
                dac = dac
            })).FirstOrDefault<Cliente>();

        }

    }
}
