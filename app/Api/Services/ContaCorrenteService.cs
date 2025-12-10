using Api.Bootstrap;
using Api.Data;
using Api.Models;
using Api.Models.ContaCorrente;
using Api.Models.Mappers;
using System.Net;

namespace Api.Services
{
    public interface IContaCorrenteService
    {
        Task<ApiResponse<SaldoResponse>> Saldo(SaldoRequest request);
        Task<ApiResponse<DepositoResponse>> SimularDeposito(Models.ContaCorrente.DepositarRequest request);
        Task<ApiResponse<DepositoResponse>> EfetivarDeposito(EfetivarDepositoRequest request);
        Task<ApiResponse<SaqueResponse>> SimularSaque(SaqueRequest request);
        Task<ApiResponse<SaqueResponse>> EfetivarSaque(EfetivarSaqueRequest request);
    }

    public class ContaCorrenteService : IContaCorrenteService
    {
        private const uint TOKEN_EXPIRES = 2;

        private readonly IClienteData clienteData;
        private readonly IContaCorrenteData contaCorrenteData;
        public ContaCorrenteService(IClienteData _clienteData, IContaCorrenteData _contaCorrenteData)
        {
            clienteData = _clienteData;
            contaCorrenteData = _contaCorrenteData;
        }



        public async Task<ApiResponse<SaldoResponse>> Saldo(SaldoRequest request)
        {
            var isValid = request.IsValid();

            if (!isValid.valid)
            {
                return new ApiResponse<SaldoResponse>()
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    message = isValid.message,
                    data = Map_SaldoResponse.Map(request)
                };
            }

            Cliente cliente = await this.Search(request.agencia, request.conta, request.dac);

            if (cliente is null)
            {
                return new ApiResponse<SaldoResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Conta corrente nao encontrada",
                    data = Map_SaldoResponse.Map(request)
                };
            }

            cliente = await this.Search(request.agencia, request.conta, request.dac, request.senha);

            if (cliente is null)
            {
                return new ApiResponse<SaldoResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Senha incorreta",
                    data = Map_SaldoResponse.Map(request)
                };
            }
            cliente.senha = request.senha;
            decimal saldo = await contaCorrenteData.Saldo(cliente);


            return new ApiResponse<SaldoResponse>()
            {
                status_code = (int)HttpStatusCode.OK,
                message = "Consulta de saldo realizada com sucesso",
                data = Map_SaldoResponse.Map(request, saldo)
            };
        }


        public async Task<ApiResponse<DepositoResponse>> SimularDeposito(Models.ContaCorrente.DepositarRequest request)
        {
            var isValid = request.IsValid(request.valor,false);

            if (!isValid.valid)
            {
                return new ApiResponse<DepositoResponse>()
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    message = isValid.message,
                    data = Map_DepositoResponse.Map(request, "")
                };
            }

            Cliente cliente = await this.Search(request.agencia, request.conta, request.dac);

            if (cliente is null)
            {
                return new ApiResponse<DepositoResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Conta corrente nao encontrada",
                    data = Map_DepositoResponse.Map(request, "")
                };
            }

            DepositoResponse response = Map_DepositoResponse.Map(request, cliente.nome);
            response.token = Token.Get(cliente.agencia, cliente.conta, cliente.dac, TOKEN_EXPIRES);

            return new ApiResponse<DepositoResponse>()
            {
                status_code = (int)HttpStatusCode.OK,
                message = "Simulacao de deposito realizada com sucesso",
                data = response
            };
        }

        public async Task<ApiResponse<DepositoResponse>> EfetivarDeposito(EfetivarDepositoRequest request)
        {
            var isValid = request.IsValid(request.token, TOKEN_EXPIRES);

            if (!isValid.valid)
            {
                return new ApiResponse<DepositoResponse>()
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    message = isValid.message,
                    data = Map_DepositoResponse.Map(request, "")
                };
            }

            isValid = request.IsValid(request.valor);

            if (!isValid.valid)
            {
                return new ApiResponse<DepositoResponse>()
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    message = isValid.message,
                    data = Map_DepositoResponse.Map(request, "")
                };
            }



            Cliente cliente = await this.Search(request.agencia, request.conta, request.dac);

            if (cliente is null)
            {
                return new ApiResponse<DepositoResponse>()
                {
                    status_code = (int)HttpStatusCode.NotFound,
                    message = "Conta corrente nao encontrada",
                    data = Map_DepositoResponse.Map(request, "")
                };
            }

            if (!await contaCorrenteData.Depositar(cliente, request.valor))
            {
                return new ApiResponse<DepositoResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Nao foi possivel realizar o deposito",
                    data = Map_DepositoResponse.Map(request, "")
                };
            }

            DepositoResponse response = Map_DepositoResponse.Map(request, cliente.nome);

            return new ApiResponse<DepositoResponse>()
            {
                status_code = (int)HttpStatusCode.OK,
                message = "Deposito efetivado com sucesso",
                data = response
            };
        }

        public async Task<ApiResponse<SaqueResponse>> SimularSaque(SaqueRequest request)
        {
            var isValid = request.IsValid(request.valor);

            if (!isValid.valid)
            {
                return new ApiResponse<SaqueResponse>()
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    message = isValid.message,
                    data = Map_SaqueResponse.Map(request)
                };
            }

            Cliente cliente = await this.Search(request.agencia, request.conta, request.dac, request.senha);

            if (cliente is null)
            {
                return new ApiResponse<SaqueResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Conta corrente nao encontrada",
                    data = Map_SaqueResponse.Map(request)
                };
            }

            if (!await contaCorrenteData.SimularSaque(cliente, request.valor))
            {
                return new ApiResponse<SaqueResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Saque nao permitido. Verifique o saldo em conta corrente.",
                    data = Map_SaqueResponse.Map(request)
                };
            }

            SaqueResponse response = Map_SaqueResponse.Map(request);
            response.token = Token.Get(cliente.agencia, cliente.conta, cliente.dac, TOKEN_EXPIRES);

            return new ApiResponse<SaqueResponse>()
            {
                status_code = (int)HttpStatusCode.OK,
                message = "Simulacao de saque realizada com sucesso",
                data = response
            };
        }

        public async Task<ApiResponse<SaqueResponse>> EfetivarSaque(EfetivarSaqueRequest request)
        {
            var isValid = request.IsValid(request.token, TOKEN_EXPIRES);

            if (!isValid.valid)
            {
                return new ApiResponse<SaqueResponse>()
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    message = isValid.message,
                    data = Map_SaqueResponse.Map(request)
                };
            }

            Cliente cliente = await this.Search(request.agencia, request.conta, request.dac);

            if (cliente is null)
            {
                return new ApiResponse<SaqueResponse>()
                {
                    status_code = (int)HttpStatusCode.NotFound,
                    message = "Conta corrente nao encontrada",
                    data = Map_SaqueResponse.Map(request)
                };
            }

            if (!await contaCorrenteData.SimularSaque(cliente, request.valor))
            {
                return new ApiResponse<SaqueResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Saque nao permitido. Verifique o saldo em conta corrente.",
                    data = Map_SaqueResponse.Map(request)
                };
            }

            if (!await contaCorrenteData.Sacar(cliente, request.valor))
            {
                return new ApiResponse<SaqueResponse>()
                {
                    status_code = (int)HttpStatusCode.InternalServerError,
                    message = "Nao foi possivel realizar o saque",
                    data = Map_SaqueResponse.Map(request)
                };
            }

            return new ApiResponse<SaqueResponse>()
            {
                status_code = (int)HttpStatusCode.OK,
                message = "Saque realizado com sucesso",
                data = Map_SaqueResponse.Map(request)
            };
        }

        private async Task<Cliente> Search(uint agencia, uint conta, uint dac)
        {
            return (await clienteData.Search(new Cliente()
            {
                agencia = agencia,
                conta = conta,
                dac = dac,
            })).FirstOrDefault<Cliente>();
        }
        private async Task<Cliente> Search(uint agencia, uint conta, uint dac, string senha)
        {
            return (await clienteData.Search(new Cliente()
            {
                agencia = agencia,
                conta = conta,
                dac = dac,
                senha = senha
            })).FirstOrDefault<Cliente>();
        }
    }
}
