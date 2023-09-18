using Financiera.DBContext.Maps;
using Financiera.Models;
using Microsoft.EntityFrameworkCore;

namespace Financiera.DBContext
{
    public class FinancieraContext: DbContext
    {
        public FinancieraContext(DbContextOptions<FinancieraContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Cuenta> Cuenta { get; set; }
        public DbSet<Movimiento> Movimiento { get; set; }
        public DbSet<TipoCuenta> TipoCuenta { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ClienteMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
