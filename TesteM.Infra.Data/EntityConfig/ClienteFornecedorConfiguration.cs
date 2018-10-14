using System.Data.Entity.ModelConfiguration;
using TesteM.Domain.Entities;

namespace TesteM.Infra.Data.EntityConfig
{
    public class ClienteFornecedorConfiguration : EntityTypeConfiguration<ClienteFornecedor>
    {
        public ClienteFornecedorConfiguration()
        {
            HasKey(c => c.Id);

            HasRequired(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId);

            HasRequired(p => p.Fornecedor)
                .WithMany()
                .HasForeignKey(p => p.FornecedorId);

        }
    }
}