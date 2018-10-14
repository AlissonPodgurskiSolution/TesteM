using System.Data.Entity.ModelConfiguration;
using TesteM.Domain.Entities;

namespace TesteM.Infra.Data.EntityConfig
{
    public class ServicoPrestadoConfiguration : EntityTypeConfiguration<ServicoPrestado>
    {
        public ServicoPrestadoConfiguration()
        {
            HasKey(c => c.Id);

            Property(c => c.DescricaoServico)
                .HasMaxLength(150);

            Property(c => c.ValorServico).IsRequired();

            Property(c => c.DataAtendimento).IsRequired();

            HasRequired(c => c.TipoServico);
            HasRequired(c => c.ClienteFornecedor);
            //HasRequired(p => p.TipoServico)
            //    .WithMany()
            //    .HasForeignKey(p => p.TipoServicoId);

            //HasRequired(p => p.ClienteFornecedor)
            //    .WithMany()
            //    .HasForeignKey(p => p.ClienteFornecedorId);

        }
    }
}