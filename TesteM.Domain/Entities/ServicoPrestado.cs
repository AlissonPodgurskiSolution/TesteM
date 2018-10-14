using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteM.Domain.Entities
{
    public class ServicoPrestado
    {
        public int Id { get; set; }
        public string DescricaoServico { get; set; }
        public DateTime DataAtendimento { get; set; }
        public Decimal ValorServico { get; set; }
        [ForeignKey("ClienteFornecedor")]
        public int ClienteFornecedorId { get; set; }

        public virtual ClienteFornecedor ClienteFornecedor { get; set; }
        [ForeignKey("TipoServico")]
        public int TipoServicoId { get; set; }
        public virtual TipoServico TipoServico { get; set; }
    }
}