using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class Clientes : ControllerBase
    {
        private readonly IClienteService clienteService;
        public Clientes(IClienteService _clienteService)
        {
            clienteService = _clienteService;
        }


        [HttpGet]
        [Route("/clientes")]
        [EndpointDescription("Lista a relacao de clientes")]
        public async Task<ActionResult<ApiResponse<List<ClientesResponse>>>> ListAll()
        {
            var result = await clienteService.ListAll();

            return StatusCode(result.status_code, result);
        }

        [HttpPost]
        [Route("/clientes")]
        [EndpointDescription("Cadastra um novo cliente")]
        public async Task<ActionResult<ApiResponse<ClienteCreateResponse>>> Create(ClienteCreateRequest request)
        {
            var result = await clienteService.Create(request);

            return StatusCode(result.status_code, result);
        }

        [HttpGet]
        [Route("/clientes/{id}")]
        [EndpointDescription("Consulta os dados do cliente")]
        public async Task<ActionResult<ApiResponse<ClientesResponse>>> Detail(uint id)
        {
            var result = await clienteService.Detail(id);

            return StatusCode(result.status_code, result);
        }

        [HttpPut]
        [Route("/clientes/{id}")]
        [EndpointDescription("Atualiza os dados do cliente")]
        public async Task<ActionResult<ApiResponse<ClienteUpdateRequest>>> Put(uint id, ClienteUpdateRequest request)
        {
            var result = await clienteService.Update(id, request);

            return StatusCode(result.status_code, result);
        }

        [HttpDelete]
        [Route("/clientes/{id}")]
        [EndpointDescription("Exclui o cliente")]
        public async Task<ActionResult<ApiResponse<int>>> Delete(uint id)
        {
            var result = await clienteService.Delete(id); 
            
            return StatusCode(result.status_code, result);
        }
    }
}