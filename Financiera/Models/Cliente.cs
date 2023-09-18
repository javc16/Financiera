using Financiera.Models.DTOs;

namespace Financiera.Models
{
    public class Cliente: Persona
    {       
        public string? Contrasena { get; set; }
        public bool Estado { get; set; }
        public List<Cuenta>? Cuentas { get; set; }



        public class Builder
        {
            private Cliente _cliente = new Cliente();

            public Builder WithIdentificacion(string? identificacion)
            {
                _cliente.Identificacion = identificacion;
                return this;
            }

            public Builder WithNombre(string? nombre)
            {
                _cliente.Nombre = nombre;
                return this;
            }

            public Builder WithGenero(Genero genero)
            {
                _cliente.Genero = genero;
                return this;
            }

            public Builder WithFechaNacimiento(DateTime fechaNacimiento)
            {
                _cliente.FechaNacimiento = fechaNacimiento;
                return this;
            }

            public Builder WithDireccion(string? direccion)
            {
                _cliente.Direccion = direccion;
                return this;
            }

            public Builder WithTelefono(string? telefono)
            {
                _cliente.Telefono = telefono!=null?telefono:String.Empty;
                return this;
            }

            public Builder WithContrasena(string? contrasena)
            {
                _cliente.Contrasena = contrasena;
                return this;
            }

            public Builder WithEstado(bool estado)
            {
                _cliente.Estado = estado;
                return this;
            }

            public Cliente Build()
            {
                return _cliente;
            }
        }
    }
}
