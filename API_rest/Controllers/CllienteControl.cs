using API_rest.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_rest.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class CllienteControl : ControllerBase
    {
        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            List<Cliente> clientes = new List<Cliente>
            {
                new Cliente
                {
                    id = "1",
                    correo = "google@gmail.com",
                    edad = "33",
                    nombre = "Bernardo"
                },
                new Cliente
                {
                    id = "2",
                    correo = "google@gmail.com",
                    edad = "33",
                    nombre = "Antonio"
                },
                new Cliente
                {
                    id = "4",
                    correo = "google@gmail.com",
                    edad = "33",
                    nombre = "Antonio"
                }
            };
            return clientes;
        }


        [HttpGet]
        [Route("listarxId")]

        public dynamic listarClientexId([FromQuery]int codigo)
        {
            return new Cliente
            {
                id = codigo.ToString(),
                correo = "google@gmail.com",
                edad = "33",
                nombre = "Bernardo"
            };
        }


        [HttpPost]
        [Route("guardar")]
        public dynamic guardarCliente(Cliente cliente)
        {
            cliente.id = "3";

            return new
            {
                success = true,
                message = "Cliente registrado",
                Results = cliente
            };
        }

        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarCliente([FromBody] Cliente cliente)
        {
            if (!Request.Headers.TryGetValue("Authorization", out var token) || token.ToString() != "marco123.")
            {
                return new
                {
                    success = false,
                    message = "Token incorrecto",
                    Results = ""
                };
            }

            return new
            {
                success = true,
                message = "Cliente eliminado",
                Results = cliente
            };
        }

    }
}

