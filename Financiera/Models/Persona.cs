namespace Financiera.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; } 
        public string Telefono { get; set; }
    }
}
