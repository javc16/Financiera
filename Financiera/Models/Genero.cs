namespace Financiera.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Cliente>? Clientes { get; set; }

    }
}
