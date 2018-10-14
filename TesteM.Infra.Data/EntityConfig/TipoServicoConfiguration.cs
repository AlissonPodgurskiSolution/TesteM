using System.Data.Entity.ModelConfiguration;
using TesteM.Domain.Entities;

namespace TesteM.Infra.Data.EntityConfig
{
    public class TipoServicoConfiguration : EntityTypeConfiguration<TipoServico>
    {
        public TipoServicoConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.Tipo)
                .HasMaxLength(50);
        }
    }
}