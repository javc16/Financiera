using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Financiera.Models;

namespace Financiera.DBContext.Maps
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().UseIdentityColumn();
            builder.Property(x => x.Identificacion).IsRequired();
            builder.Property(x => x.Nombre).IsRequired();
            builder.Property(x => x.Telefono).IsRequired();
            builder.Property(x => x.Genero).IsRequired();
            builder.Property(x => x.Contrasena);
            builder.Property(x => x.Direccion).IsRequired();
            builder.Property(x => x.Estado);
        }
    }

}
