namespace Financiera.Models.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string? Identificacion { get; set; }
        public string? Nombre { get; set; }
        public int GeneroId { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Contrasena { get; set; }
        public bool Estado { get; set; }

        public static ClienteDTO FromModelToDTO(Cliente cliente)
        {

            var currentAge = DateTime.Today.Year - cliente.FechaNacimiento.Year;
            return cliente != null ? new ClienteDTO
            {
                Id = cliente.Id,
                Identificacion = cliente.Identificacion,
                Nombre = cliente.Nombre,
                Edad = DateTime.Today < cliente.FechaNacimiento.AddYears(currentAge) ? currentAge - 1: currentAge,
                GeneroId = cliente.Genero.Id,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Contrasena = cliente.Contrasena,
                FechaNacimiento = cliente.FechaNacimiento,
                Estado = cliente.Estado
            } : new ClienteDTO();
        }

        public static IEnumerable<ClienteDTO> FromModelToDTO(IEnumerable<Cliente> cliente)
        {
            if (cliente == null)
            {
                return new List<ClienteDTO>();
            }
            List<ClienteDTO> clienteData = new List<ClienteDTO>();

            foreach (var item in cliente)
            {
                clienteData.Add(FromModelToDTO(item));
            }

            return clienteData;
        }

        public static Cliente FromDtoToModel(ClienteDTO clienteDTO,Genero genero)
        {
            return clienteDTO != null ? new Cliente.Builder().WithContrasena(clienteDTO.Contrasena)
                .WithEstado(clienteDTO.Estado).WithFechaNacimiento(clienteDTO.FechaNacimiento)
                .WithDireccion(clienteDTO.Direccion).WithIdentificacion(clienteDTO.Identificacion)
                .WithNombre(clienteDTO.Nombre).WithTelefono(clienteDTO.Telefono).WithGenero(genero).Build() : new Cliente();
        }
    }
}
