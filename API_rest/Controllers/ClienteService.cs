using API_rest.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

public class ClienteService
{
    private readonly DatabaseService _dbService;

    public ClienteService(DatabaseService dbService)
    {
        _dbService = dbService;
    }

    public IEnumerable<Cliente> ListarClientes()
    {
        using (var connection = _dbService.CreateConnection())
        {
            connection.Open();
            var clientes = new List<Cliente>();
            var query = "SELECT * FROM clientes";

            using (var command = new MySqlCommand(query, (MySqlConnection)connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        id = reader["id"].ToString(),
                        nombre = reader["nombre"].ToString(),
                        edad = reader["edad"].ToString(),
                        correo = reader["correo"].ToString()
                    });
                }
            }
            return clientes;
        }
    }

    public Cliente ObtenerClientePorId(int id)
    {
        using (var connection = _dbService.CreateConnection())
        {
            connection.Open();
            var query = "SELECT * FROM clientes WHERE id = @id";

            using (var command = new MySqlCommand(query, (MySqlConnection)connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cliente
                        {
                            id = reader["id"].ToString(),
                            nombre = reader["nombre"].ToString(),
                            edad = reader["edad"].ToString(),
                            correo = reader["correo"].ToString()
                        };
                    }
                }
            }
        }
        return null;
    }
    public bool GuardarCliente(Cliente cliente)
    {
        using (var connection = _dbService.CreateConnection())
        {
            connection.Open();
            var query = "INSERT INTO clientes (nombre, edad, correo) VALUES (@nombre, @edad, @correo)";

            using (var command = new MySqlCommand(query, (MySqlConnection)connection))
            {
                command.Parameters.AddWithValue("@nombre", cliente.nombre);
                command.Parameters.AddWithValue("@edad", cliente.edad);
                command.Parameters.AddWithValue("@correo", cliente.correo);

                int filasAfectadas = command.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }

    public bool EliminarCliente(int id)
    {
        using (var connection = _dbService.CreateConnection())
        {
            connection.Open();
            var query = "DELETE FROM clientes WHERE id = @id";

            using (var command = new MySqlCommand(query, (MySqlConnection)connection))
            {
                command.Parameters.AddWithValue("@id", id);

                int filasAfectadas = command.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }

}
