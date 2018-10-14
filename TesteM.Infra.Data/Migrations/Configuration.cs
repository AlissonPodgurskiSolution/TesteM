using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using TesteM.Domain.Entities;
using TesteM.Infra.Data.Context;

namespace TesteM.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TesteMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TesteMContext context)
        {
            //This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.E.g.
            var clientes = new List<Cliente>
            {
                new Cliente() {Nome = "Jo�o", Bairro = "Testo Salto", Cidade = "Blumenau", Estado = "SC"},
                new Cliente() {Nome = "Maria", Bairro = "Testo Salto", Cidade = "Blumenau", Estado = "SC"},
                new Cliente() {Nome = "Jos�", Bairro = "Oficinas", Cidade = "Ponta Grossa", Estado = "PR"},
                new Cliente() {Nome = "Alisson", Bairro = "Oficinas", Cidade = "Ponta Grossa", Estado = "PR"},
                new Cliente() {Nome = "Adri�li", Bairro = "Oficinas", Cidade = "Ponta Grossa", Estado = "PR"},
                new Cliente() {Nome = "Allan", Bairro = "Amizade", Cidade = "Jaragu�", Estado = "SC"}
            };


            context.Clientes.AddRange(clientes);

            var fornecedores = new List<Fornecedor>
            {
                new Fornecedor() {Nome = "Azir"},
                new Fornecedor() {Nome = "Ahri"},
                new Fornecedor() {Nome = "Yusuki"},
                new Fornecedor() {Nome = "Urameshi"},
                new Fornecedor() {Nome = "Hiei"},
                new Fornecedor() {Nome = "Kurama"},
                new Fornecedor() {Nome = "Goku"}
            };

            context.Fornecedores.AddRange(fornecedores);
            var clienteFornecedor = new List<ClienteFornecedor>();

            foreach (var fornecedor in fornecedores)
            {
                foreach (var cliente in clientes)
                {
                    clienteFornecedor.Add(new ClienteFornecedor() { Fornecedor = fornecedor, Cliente = cliente });
                }
            }

            context.ClientesFornecedores.AddRange(clienteFornecedor);



            var tiposServicos = new List<TipoServico>
            {
                new TipoServico() {Tipo = "Conserto eletr�nico"},
                new TipoServico() {Tipo = "Servi�os Gerais"},
                new TipoServico() {Tipo = "Manuten��o Hidr�ulica"},
                new TipoServico() {Tipo = "Instala��o El�trica"},
                new TipoServico() {Tipo = "Jardinagem"}
            };

            context.TiposServicos.AddRange(tiposServicos);

            var servicosPrestados = new List<ServicoPrestado>
            {
                new ServicoPrestado() {TipoServico = tiposServicos.FirstOrDefault(), ClienteFornecedor = clienteFornecedor.FirstOrDefault(), DataAtendimento = DateTime.Now, ValorServico = 100, DescricaoServico = "Primeiro Servi�o!"}
            };

            context.ServicosPrestados.AddRange(servicosPrestados);


            base.Seed(context);

        }
    }
}
