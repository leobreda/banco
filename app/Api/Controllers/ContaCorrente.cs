using Api.Models;
using Api.Models.ContaCorrente;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ContaCorrente : ControllerBase
    {
        private readonly IContaCorrenteService contaCorrenteService;
        public ContaCorrente(IContaCorrenteService _contaCorrenteService)
        {
            contaCorrenteService = _contaCorrenteService;
        }

        [HttpPost]
        [Route("/clientes/saldo")]
        [EndpointDescription("Consulta o saldo em conta corrente. Necessario o uso de senha")]
        public async Task<ActionResult<ApiResponse<DepositoResponse>>> Saldo([FromBody] SaldoRequest request)
        {
            var result = await contaCorrenteService.Saldo(request);
            return StatusCode(result.status_code, result);
        }

        [HttpPost]
        [Route("/clientes/depositar")]
        [EndpointDescription("Simula o deposito em conta corrente. Necessario pra gerar o token")]
        public async Task<ActionResult<ApiResponse<DepositoResponse>>> SimularDeposito([FromBody] DepositarRequest request)
        {
            var result = await contaCorrenteService.SimularDeposito(request);
            return StatusCode(result.status_code, result);
        }

        [HttpPost]
        [Route("/clientes/depositar/efetivar")]
        [EndpointDescription("Efetiva o deposito em conta corrente. Necessario o uso de token")]
        public async Task<ActionResult<ApiResponse<DepositoResponse>>> EfetivarDeposito([FromBody] EfetivarDepositoRequest request)
        {

            var result = await contaCorrenteService.EfetivarDeposito(request);
            return StatusCode(result.status_code, result);
        }

        [HttpPost]
        [Route("/clientes/saque")]
        [EndpointDescription("Simula o saque em conta corrente. Necessario pra gerar o token")]
        public async Task<ActionResult<ApiResponse<SaqueResponse>>> SimularSaque([FromBody] SaqueRequest request)
        {
            var result = await contaCorrenteService.SimularSaque(request);
            return StatusCode(result.status_code, result);
        }

        [HttpPost]
        [Route("/clientes/saque/efetivar")]
        [EndpointDescription("Efetiva o saque em conta corrente. Necessario o uso de token")]
        public async Task<ActionResult<ApiResponse<SaqueResponse>>> EfetivarSaque([FromBody] EfetivarSaqueRequest request)
        {
            var result = await contaCorrenteService.EfetivarSaque(request);
            return StatusCode(result.status_code, result);
        }
    }
}
