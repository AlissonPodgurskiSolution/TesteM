using System.Data.Entity.ModelConfiguration;
using TesteM.Domain.Entities;

namespace TesteM.Infra.Data.EntityConfig
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Nome)
                .HasMaxLength(150);

            Property(c => c.Bairro)
                .HasMaxLength(150);

            Property(c => c.Cidade)
                .HasMaxLength(150);

            Property(c => c.Estado)
                .HasMaxLength(150);

        }
    }
}