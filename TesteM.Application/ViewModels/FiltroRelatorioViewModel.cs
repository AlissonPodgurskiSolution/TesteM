using System;

namespace TesteM.Application.ViewModels
{
    public class FiltroRelatorioViewModel
    {
        public DateTime? PeriodoDe { get; set; }
        public DateTime? PeriodoAte { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? ClienteId { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int? TipoServicoId { get; set; }
        public int? FornecedorId { get; set; }
    }
}