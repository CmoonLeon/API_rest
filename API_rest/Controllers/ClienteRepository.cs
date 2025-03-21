using API_rest.Models;

public class ClienteRepository
{
    private List<Cliente> _clientes = new List<Cliente>
    {
        new Cliente { id = "1", correo = "google@gmail.com", edad = "31", nombre = "Bernardo" },
        new Cliente { id = "2", correo = "google@gmail.com", edad = "32", nombre = "Antonio" },
        new Cliente { id = "4", correo = "google@gmail.com", edad = "34", nombre = "Antonio" },
        new Cliente { id = "5", correo = "Juanjo@gmail.com", edad = "35", nombre = "Juan José"}
    };

    public IEnumerable<Cliente> GetClientes()
    {
        return _clientes;
    }

    public Cliente GetClientePorId(int id)
    {
        return _clientes.FirstOrDefault(c => int.Parse(c.id) == id);
    }
}
