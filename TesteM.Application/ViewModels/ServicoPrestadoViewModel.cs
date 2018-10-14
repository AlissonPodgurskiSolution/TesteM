using System;
using System.ComponentModel.DataAnnotations;

namespace TesteM.Application.ViewModels
{
    public class ServicoPrestadoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Descrição do Serviço")]
        [Required(ErrorMessage = "A descrição do serviço é obrigatório", AllowEmptyStrings = false)]
        public string DescricaoServico { get; set; }

        [Display(Name = "Data do Atendimento")]
        [Required(ErrorMessage = "A Data do Atendimento é obrigatório", AllowEmptyStrings = false)]
        public DateTime DataAtendimento { get; set; }

        public string DataAtendimentoddMMyyyy {
            get { return DataAtendimento.ToShortDateString(); }
        }

        [Display(Name = "Valor do Serviço")]
        [Required(ErrorMessage = "O Valor do Serviço é obrigatório", AllowEmptyStrings = false)]
        public decimal ValorServico { get; set; }

        public int ClienteFornecedorId { get; set; }
        public int FornecedorId { get; set; }
        public int ClienteId { get; set; }
        public ClienteFornecedorViewModel ClienteFornecedor { get; set; }
        public int TipoServicoId { get; set; }
        public TipoServicoViewModel TipoServico { get; set; }
    }
}