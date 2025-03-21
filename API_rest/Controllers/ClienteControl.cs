using API_rest.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_rest.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteControl : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteControl(ClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        //lista completa de clientes
        [HttpGet]
        [Route("listar")]
        public IActionResult listarCliente()
        {
            var clientes = _clienteService.ListarClientes();
            var clientesDTO = _mapper.Map<List<ClienteDTO>>(clientes);

            return Ok(clientesDTO);
        }

        //lista de cliente por id
        [HttpGet]
        [Route("listarxId")]
        public IActionResult listarClientexId([FromQuery] int id)
        {
            var cliente = _clienteService.ObtenerClientePorId(id);
            if (cliente == null)
                return NotFound("Cliente no encontrado");

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
            return Ok(clienteDTO);
        }

        //para guardar un cliente
        [HttpPost]
        [Route("guardar")]
        public IActionResult guardarCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
            {
                return BadRequest("Los datos del cliente son inválidos.");
            }

            // aquí se mapea el DTO al modelo de entidad
            var cliente = _mapper.Map<Cliente>(clienteDTO);

            // se llama al servicio para guardar el cliente
            var resultado = _clienteService.GuardarCliente(cliente);

            if (!resultado)
                return BadRequest("No se pudo guardar el cliente.");

            return CreatedAtAction(nameof(listarClientexId), new { id = cliente.id }, _mapper.Map<ClienteDTO>(cliente));
        }

        //para eliminar un cliente
        [HttpDelete]
        [Route("eliminar")]
        public IActionResult eliminarCliente([FromQuery] int id)
        {
            var resultado = _clienteService.EliminarCliente(id);

            if (resultado)
                return Ok("Cliente eliminado exitosamente.");

            return NotFound("Cliente no encontrado.");
        }

        //espacio por si quiero poner un editor de clientes 
        //nota: no lo pongas
        //nota de la nota: ponlo, es más facil que eliminar y volver a guardar
        //tiempo invertido en este proyecto: 10 horas (investigación, escritura de código, pruebas)
    }
}