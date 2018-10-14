using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TesteM.Domain.Entities;
using TesteM.Infra.Data.EntityConfig;

namespace TesteM.Infra.Data.Context
{
    public class TesteMContext : DbContext
    {
        public TesteMContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<ClienteFornecedor> ClientesFornecedores { get; set; }
        public DbSet<TipoServico> TiposServicos { get; set; }
        public DbSet<ServicoPrestado> ServicosPrestados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new FornecedorConfiguration());
            modelBuilder.Configurations.Add(new ClienteFornecedorConfiguration());
            modelBuilder.Configurations.Add(new TipoServicoConfiguration());
            modelBuilder.Configurations.Add(new ServicoPrestadoConfiguration());



        }

    }
}
