using System.Data.Entity.ModelConfiguration;
using TesteM.Domain.Entities;

namespace TesteM.Infra.Data.EntityConfig
{
    public class FornecedorConfiguration : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Nome)
                .HasMaxLength(150);
        }
    }
}